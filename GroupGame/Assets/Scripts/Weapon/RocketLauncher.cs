﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    private PooledGameObjects pgo;          //the script handling pooling. to be called when a bullet is "created"
    private float fire_rate = 0.3f;         //How long for a singla fire exists
    private int can_fire = 1;               //If the player fired

    private Camera playerCam;

    public GameObject Rocket;      //The rocket object wait to be initiated
    public float rocket_speed = 50.0f;

    private int bulletId; //workaround until this is changed to extend gun class.

    // Use this for initialization
    void Start()
    {
        pgo = GameObject.Find("PooledBullets").GetComponent<PooledGameObjects>();
        bulletId = pgo.InitializeObjectType(Rocket);
        

        Debug.Log(pgo.ToString());
        //playerCam = transform.parent.transform.GetChild(0).GetComponent<Camera>();
        
    }


    // Call for each frame
    void Update()
    {
        //Check if the player hit the fire button
        if (Input.GetButton("Fire1") && can_fire ==1)
        {
            GameObject CR = pgo.GetPooledObject(bulletId);
            CR.transform.position = transform.position;
            CR.transform.rotation = transform.rotation;
            CR.SetActive(true);
            //GameObject CR = Instantiate(Rocket, transform.position, transform.rotation);        //Instantiate the rocket object at current position and current angle
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
