using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour {

    private LineRenderer laser;

    private new Camera camera;      //Using new to hide the laser's camera object (This was suggested by unity, if the camera has any laser)

    private float exist_time = 0.2f;

    private int exist_flag = 0;



	// Use this for initialization
	void Start () {
        laser = GetComponent<LineRenderer>();       //Get the laser trace

        camera = this.transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>();     //First find the parent object, then find the camera object, then get the main camera
	}
	
	// Update is called once per frame
	void Update () {
		
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
                //If the target has a collider (hitable)
                if (hit.collider != null)
                {
                    Debug.Log("Hit! \n");
                    //Shoot a laser to target
                    Debug.Log(hit.point);
                    laser.SetPosition(1, hit.point);        //Point to target point
                    exist_flag = 1;
                }
            }           
        }

        if(exist_flag == 1)
        {
            exist_time -= Time.deltaTime;
        }

        //Check if the laser should be turned off
        if(exist_time <= 0)
        {
            laser.SetPosition(1, laser.GetPosition(0));     //Set the laser with 0 length

            exist_flag = 0;

            exist_time = 0.2f;
        }
	}

    
}
