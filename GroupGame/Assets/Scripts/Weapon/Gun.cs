using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {
    
    private long lastFireMS = 0;
    
    protected bool customFireSequence = false; //set this to true in child to disable default firing functionality

    public GameObject bulletObj;
    private int bulletId = -1;
    
    public float coolDownMS = 100f;


    public Camera playerCam;      //Using new to hide the laser's camera object (This was suggested by unity, if the camera has any laser)
    public GameObject aim;         //Used to help aiming

	public virtual void Start () {
        if(playerCam == null) { //if unset, try to pull it from the parent(player)
            playerCam = this.transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
            aim = playerCam.transform.GetChild(0).gameObject;
            
        }
		this.gameObject.layer = Bullet.BULLET_IGNORE_LAYER; //preventing the bullets from colliding with the guns
        
        bulletId = PooledGameObjects.InitializeObjectType(bulletObj);
    }

    public void SetCamera(Camera c) {
        playerCam = c;
        aim = playerCam.transform.GetChild(0).gameObject;
    }

    public void Reset() {
        lastFireMS = 0;
    }
    

    protected virtual void FireProjectile() {
        //doesn't fire if fire button isn't set or the gun is still cooling down
        if (!Input.GetButton("Fire1") || (getCurrentMS() - lastFireMS) < coolDownMS) {
            return;
        }

        //grabbing bullet object from the pooled game objects.
        GameObject bullet = PooledGameObjects.GetPooledObject(bulletId);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        bullet.transform.parent = null;

        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)){
            Vector3 dir = hit.point - aim.transform.position;
            bullet.GetComponent<Rigidbody>().velocity = dir.normalized;
        }
        else {
            bullet.GetComponent<Rigidbody>().velocity = playerCam.transform.forward;    //speed multiplier added inside bullet object.
        }

        bullet.GetComponent<Bullet>().Reset();
        

        lastFireMS = getCurrentMS();
    }

    private void FixedUpdate() {
        if (!customFireSequence) {
            FireProjectile();
        }
    }

    protected long getCurrentMS() {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}
