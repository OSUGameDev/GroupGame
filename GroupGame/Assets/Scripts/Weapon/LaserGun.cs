using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour {

    private LineRenderer laser;

    private new Camera camera;      //Using new to hide the laser's camera object (This was suggested by unity, if the camera has any laser)
    private GameObject aim;         //Used to help aiming

    private float exist_time = 0.2f;        //How long for a singla fire exists

    private int exist_flag = 0;     //If the player fired



	// Use this for initialization
	void Start () {
        laser = GetComponent<LineRenderer>();       //Get the laser trace

        camera = this.transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();     //First find the parent object, then find the camera object, then get the main camera

        aim = camera.transform.GetChild(0).gameObject;      //Get the aiming object, this object will stay in line relate to the camera, help the gun to find the target point
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
        //Check if the player hit the fire button
        if(Input.GetButton("Fire1") && exist_flag == 0)
        {
            //Cast a ray from center of the camera
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            //Find the target point
            RaycastHit hit;
            //If the ray hit something
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Fire! \n");
                
                if (hit.collider != null)           //If the target has a collider (hitable)
                {
                    Debug.Log("Hit! \n");

                    //Shoot a laser to target
                    Debug.Log(hit.point);
                    laser.SetPosition(0, transform.position);   //Set the origin point to current location
                    laser.SetPosition(1, hit.point);            //Point to target point
                    exist_flag = 1;         //Set the fire flag

                    //Check if hit the enemy
                    GameObject target = hit.collider.transform.gameObject;
                    if (target.tag == "Target Tester")
                    {
                        Debug.Log("Hit the target!");
                        target.GetComponent<EnemyHealth>().TakeDamage(20);      //Make damage to target
                    }
                }
                else            //If the player shot something that 
                {

                }
            }           
        }

        if(exist_flag == 1)
        {
            exist_time -= Time.deltaTime;       //The current exist timer of a single fire
        }

        //Check if the laser should be turned off
        if(exist_time <= 0)
        {
            laser.SetPosition(1, laser.GetPosition(0));     //Set the laser with 0 length

            exist_flag = 0;

            exist_time = 0.2f;      //Reset the fire timer
        }
	}

    
}
