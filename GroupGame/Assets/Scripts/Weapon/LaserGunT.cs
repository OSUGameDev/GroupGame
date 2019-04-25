using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Gun {

    private LineRenderer laser;

<<<<<<< HEAD:GroupGame/Assets/Scripts/Weapon/LaserGunT.cs
    private float exist_time = 0.2f;        //How long for a singla fire exists
=======
    private float exist_time = 0.1f;        //How long for a singla fire exists
>>>>>>> origin/Game-Mechanics-Michael:GroupGame/Assets/Scripts/Weapon/LaserGun.cs
    private int exist_flag = 0;     //If the player fired

    private Camera playerCam;


<<<<<<< HEAD:GroupGame/Assets/Scripts/Weapon/LaserGunT.cs
    // Use this for initialization
    protected override void Start () {
        base.Start();

        laser = GetComponent<LineRenderer>();       //Get the laser trace
	}


    //called in FixedUpdate
    protected override void FireProjectile() {
        //Check if the player hit the fire button
        if (Input.GetButton("Fire1") && exist_flag == 0) {
            //Cast a ray from center of the camera
            Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            //Find the target point
            RaycastHit hit;
            //If the ray hit something
            if (Physics.Raycast(ray, out hit)) {
                Debug.Log("Fire! \n");
<<<<<<< HEAD
                //If the target has a collider (hitable)
                if (hit.collider != null) {
=======
                
                if (hit.collider != null)           //If the target has a collider (hitable)
                {
>>>>>>> origin/Game-Mechanics-Michael
                    Debug.Log("Hit! \n");

                    //Shoot a laser to target
                    Debug.Log(hit.point);
                    laser.SetPosition(0, transform.position);   //Set the origin point to current location
                    laser.SetPosition(1, hit.point);            //Point to target point
                    exist_flag = 1;         //Set the fire flag

                    //Check if hit the enemy
                    GameObject target = hit.collider.transform.gameObject;
                    if (target.tag == "Target Tester") {
                        Debug.Log("Hit the target!");
                        target.GetComponent<EnemyHealth>().TakeDamage(20);      //Make damage to target
                    }
                }
<<<<<<< HEAD
            }
=======
                else            //If the player shot something that 
                {

                }
            }           
>>>>>>> origin/Game-Mechanics-Michael
        }

        if (exist_flag == 1) {
=======

    // Use this for initialization
    void Start ()
    {
        laser = GetComponent<LineRenderer>();       //Get the laser trace

        playerCam = transform.parent.transform.GetChild(0).GetComponent<Camera>();
	}


    //called in FixedUpdate
    void Update ()
    {
		//Check if the player hit the fire button
		if (Input.GetButton ("Fire1") && exist_flag == 0)
        {
			//Cast a ray from center of the camera
			Ray ray = playerCam.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0));
			//Find the target point
			RaycastHit hit;
			//If the ray hit something
			if (Physics.Raycast (ray, out hit))
            {
				Debug.Log ("Fire! \n");
				//If the target has a collider (hitable)
				if (hit.collider != null)
                {           //If the target has a collider (hitable)
					Debug.Log ("Hit! \n");

					//Shoot a laser to target
					Debug.Log (hit.point);
					laser.SetPosition (0, transform.position);   //Set the origin point to current location
					laser.SetPosition (1, hit.point);            //Point to target point
					exist_flag = 1;         //Set the fire flag

					//Check if hit the enemy
					GameObject target = hit.collider.transform.gameObject;
					if (target.tag == "Target Tester")
                    {
						Debug.Log ("Hit the target!");
						target.GetComponent<EnemyHealth> ().TakeDamage (20);      //Make damage to target
					}
				}
			}
		}

        if (exist_flag == 1)
        {
>>>>>>> origin/Game-Mechanics-Michael:GroupGame/Assets/Scripts/Weapon/LaserGun.cs
            exist_time -= Time.deltaTime;       //The current exist timer of a single fire
        }

        //Check if the laser should be turned off
<<<<<<< HEAD:GroupGame/Assets/Scripts/Weapon/LaserGunT.cs
        if (exist_time <= 0) {
=======
        if (exist_time <= 0)
        {
>>>>>>> origin/Game-Mechanics-Michael:GroupGame/Assets/Scripts/Weapon/LaserGun.cs
            laser.SetPosition(1, laser.GetPosition(0));     //Set the laser with 0 length

            exist_flag = 0;

            exist_time = 0.2f;      //Reset the fire timer
        }
    }
    
}
