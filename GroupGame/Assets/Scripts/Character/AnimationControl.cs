using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

    private Animator anim;      //The current animator object

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();     //Get the animator object
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetAxis("Verticle") > 0)
        {
            anim.Play("RUN00_F");
        }

        if(Input.GetButtonDown("Jump"))
        {
            anim.Play("JUMP00");
        }

	}
}
