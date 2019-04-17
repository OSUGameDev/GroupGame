using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

    protected Camera playerCam;      //Using new to hide the laser's camera object (This was suggested by unity, if the camera has any laser)
    protected GameObject aim;         //Used to help aiming

	protected virtual void Start () { 
        playerCam = this.transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();     //First find the parent object, then find the camera object, then get the main camera
        aim = playerCam.transform.GetChild(0).gameObject;      //Get the aiming object, this object will stay in line relate to the camera, help the gun to find the target point
    }

    // Update is called once per frame
    protected virtual void Update () {
		
	}

    protected abstract void FireProjectile();

    private void FixedUpdate() {
        FireProjectile();
    }

    protected long getCurrentMS() {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}
