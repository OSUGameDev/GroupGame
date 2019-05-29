using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun {
    //TODO: Change this to use a projectile or something.
    //currently it's just overriding all the gun functions to disable the default functionality

    private LineRenderer laser;

    private float exist_time = 0.1f;        //How long for a singla fire exists
    private int exist_flag = 0;     //If the player fired

    public new void Reset() {
        base.Reset();

        customFireSequence = true;
    }

    // Use this for initialization
    public override void Start() {
        Reset();

        laser = GetComponent<LineRenderer>();       //Get the laser trace
    }

    void Update() {
        if (Input.GetButton("Fire1") && exist_flag == 0) {
            //Cast a ray from center of the camera
            Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            //Find the target point
            RaycastHit hit;
            //If the ray hit something

            if (Physics.Raycast(ray, out hit)) {
                //If the target has a collider (hitable)
                if (hit.collider != null) {           //If the target has a collider (hitable)

                    //Shoot a laser to target
                    laser.SetPosition(0, transform.position);   //Set the origin point to current location
                    laser.SetPosition(1, hit.point);            //Point to target point
                    exist_flag = 1;         //Set the fire flag

                    //Check if hit the enemy
                    GameObject target = hit.collider.transform.gameObject;
                    if (target.tag == "Target Tester") {
                        target.GetComponent<EnemyHealth>().TakeDamage(20);      //Make damage to target
                    }
                }
            }
        }

        if (exist_flag == 1) {
            exist_time -= Time.deltaTime;       //The current exist timer of a single fire
        }

        //Check if the laser should be turned off
        if (exist_time <= 0) {
            laser.SetPosition(1, laser.GetPosition(0));     //Set the laser with 0 length

            exist_flag = 0;

            exist_time = 0.2f;      //Reset the fire timer
        }
    }

}