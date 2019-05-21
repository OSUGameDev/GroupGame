﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRocket : MonoBehaviour {

    private float life_time = 0.0f;         //How is thie object already existed in the scene?
    private float max_life = 5.0f;          //The max time of this object could exists in the scene
    private Collider physical_collider;
    public GameObject explosion;            //This will load the explosion effect


	// Use this for initialization
	void Start ()
    {
        physical_collider = GetComponent<CapsuleCollider>();        //Get the current object's collider
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        life_time += Time.deltaTime;            //Count the time
        if(life_time >= max_life)               //If exists too long
        {
            life_time = 0.0f;
            this.gameObject.SetActive(false);
            //Destroy(gameObject);        //Delete this object
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Rocket hit!");
        Debug.Log(collision.collider.name);
        if(life_time >= 0.5f)      //This was used to prevent collide with the player's collider, will be fixed later
        {
            Instantiate(explosion, transform.position, transform.rotation);
            life_time = 0.0f;
            this.gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
