﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float power;                //The explosion power
    public float explosion_radius;      //The explosion radius
    private float explosion_time = 10.0f;       //The time that this object will exist in the scene

    // Use this for initialization
    void Start (){
		Reset ();
    }

	public void Reset(){
		Collider[] explod_list = Physics.OverlapSphere(transform.position, explosion_radius);           //Get the affected object by this explision inside the radius
		foreach (Collider explod_object in explod_list)         //For each object
		{
			if (explod_object.gameObject.GetComponent<Rigidbody>())         //If the target object has a rigidbody, which means they could be forced away!
			{
				explod_object.gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, explosion_radius);          //Add a explosion effect on them
			}
		}
		explosion_time = 10f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        explosion_time -= Time.deltaTime;       //Count the time for each frame
        if(explosion_time <= 0)     //If the time to delete the object
        {
			this.gameObject.SetActive(false);
        }
	}
}
