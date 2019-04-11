using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 1000f;

	Rigidbody playerBody;
	Vector3 moveDirection;

	void Awake(){
		playerBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalMovement = Input.GetAxisRaw ("Horizontal");
		float verticalMovement = Input.GetAxisRaw ("Vertical");

		moveDirection = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
	}


	//Physics updater
	void FixedUpdate(){ 
		Move ();
	}

	void Move(){
		Vector3 yVelocity = new Vector3 (0, playerBody.velocity.y, 0);
		//stores and then readds the yvelocity to allow the player to actually fall

		playerBody.velocity = moveDirection * walkSpeed * Time.deltaTime;
		playerBody.velocity += yVelocity;
	}

}
