using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

    private long lastFireMS = 0;
    protected bool customFireSequence = false; //set this to true in child to disable default firing functionality

    public Rigidbody bulletObj;
    
    public float coolDownMS = 100f;
    public float bulletSpeed = 5000f;


    protected Camera playerCam;      //Using new to hide the laser's camera object (This was suggested by unity, if the camera has any laser)
    protected GameObject aim;         //Used to help aiming

	protected virtual void Start () {
        //First find the parent object, then find the camera object, then get the main camera
        playerCam = this.transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();     
        aim = playerCam.transform.GetChild(0).gameObject;      //Get the aiming object, this object will stay in line relate to the camera, help the gun to find the target point
    }

    // Update is called once per frame
    protected virtual void Update () {
		
	}

    protected virtual void FireProjectile() {
        //doesn't fire if fire button isn't set or the gun is still cooling down
        if (!Input.GetButton("Fire1") || (getCurrentMS() - lastFireMS) < coolDownMS) {
            return;
        }

        
        Rigidbody bulletClone = (Rigidbody)Instantiate(bulletObj, transform.position, transform.rotation);
        bulletClone.velocity = playerCam.transform.forward;

        lastFireMS = getCurrentMS();
    }

    private void FixedUpdate() {
        FireProjectile();
    }

    protected long getCurrentMS() {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}
