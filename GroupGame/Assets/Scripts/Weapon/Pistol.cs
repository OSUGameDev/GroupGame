using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun {
    public float coolDownMS = 250f;

    public float bulletSpeed = 10000f;
    public Rigidbody bulletModel;


    private long lastFireMS = 0;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

    protected override void FireProjectile() {
        if (Input.GetButton("Fire1") && (getCurrentMS() - lastFireMS) >= coolDownMS){
            Rigidbody bulletClone = (Rigidbody)Instantiate(bulletModel, transform.position, transform.rotation);
            bulletClone.velocity = playerCam.transform.forward;

            lastFireMS = getCurrentMS();
        }
    }
}
