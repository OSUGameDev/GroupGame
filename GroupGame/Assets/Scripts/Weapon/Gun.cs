using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

    private PooledGameObjects pgo;

    private long lastFireMS = 0;
    protected bool customFireSequence = false; //set this to true in child to disable default firing functionality

    public GameObject bulletObj;
    private int bulletId = -1;
    
    public float coolDownMS = 100f;


    public Camera playerCam;      //Using new to hide the laser's camera object (This was suggested by unity, if the camera has any laser)
    public GameObject aim;         //Used to help aiming

	protected virtual void Start () {
        pgo = GameObject.Find("PooledBullets").GetComponent<PooledGameObjects>();
        bulletId = pgo.InitializeObjectType(bulletObj);

        Debug.Log(pgo.ToString());

        //First find the parent object, then find the camera object, then get the main camera
        //playerCam = this.transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();     
        //aim = playerCam.transform.GetChild(0).gameObject;      //Get the aiming object, this object will stay in line relate to the camera, help the gun to find the target point
    }

    // Update is called once per frame
    protected virtual void Update () {
		
	}

    protected virtual void FireProjectile() {
        //doesn't fire if fire button isn't set or the gun is still cooling down
        if (!Input.GetButton("Fire1") || (getCurrentMS() - lastFireMS) < coolDownMS) {
            return;
        }


        GameObject bullet = pgo.GetPooledObject(bulletId);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        bullet.transform.parent = null;

        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            //Debug.Log("Hit something!");
            //Debug.Log((hit.point - transform.position));
            Vector3 dir = hit.point - aim.transform.position;
            bullet.GetComponent<Rigidbody>().velocity = dir.normalized;
        }
        else
        {
            bullet.GetComponent<Rigidbody>().velocity = playerCam.transform.forward;    //speed multiplier added inside bullet object.
        }
        bullet.GetComponent<Bullet>().Reset();
        

        lastFireMS = getCurrentMS();
    }

    private void FixedUpdate() {
        FireProjectile();
    }

    protected long getCurrentMS() {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}
