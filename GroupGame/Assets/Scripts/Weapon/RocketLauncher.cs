using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    private float fire_rate = 0.3f;         //How long for a singla fire exists
    private int can_fire = 1;               //If the player fired

    private Camera playerCam;

    public GameObject Rocket;      //The rocket object wait to be initiated
    public float rocket_speed = 50.0f;



    // Use this for initialization
    void Start()
    {
        playerCam = transform.parent.transform.GetChild(0).GetComponent<Camera>();
    }


    // Call for each frame
    void Update()
    {
        //Check if the player hit the fire button
        if (Input.GetButton("Fire1") && can_fire ==1)
        {
            GameObject CR = Instantiate(Rocket, transform.position, transform.rotation);        //Instantiate the rocket object at current position and current angle
            CR.GetComponent<Rigidbody>().velocity = transform.forward * rocket_speed;       //Set the speed
            can_fire = 0;
        }

        if(can_fire == 0)
        {
            fire_rate -= Time.deltaTime;
            if(fire_rate <= 0)
            {
                fire_rate = 0.1f;
                can_fire = 1;
            }
        }

    }

}
