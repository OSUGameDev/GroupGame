using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///PLEASE READ BEFORE USING:
///		This script should be placed on an empty gameobject with two children:
///		the first should be the complete gameobject and the second should be an
///		empty gameobject with all the broken pieces (shards) of the gameobject as children.
///		The complete gameobject should have a Collider, while each shard should have
///		a Convex Mesh Collider and a Rigibody.

public class DestroyMe : MonoBehaviour {

	public float explosionForce = 400; 	//the force of the explosion

	public float explosionSize = 100; 	//the radius of the explosion in game units

	private Vector3 explosionLocation; 	//The center of the explosion, from which the force radiates

	private GameObject completeObject; 	//The object that represents the complete asset, before "destroying"

	private GameObject brokenObject; 	//The parent of all the shards of the asset, which spawn when "destroyed"


	// Use this for initialization
	void Start () {
		
		completeObject = this.transform.GetChild(0).gameObject; 	//instantiate the complete asset to the first child of the object with this script
		brokenObject = this.transform.GetChild(1).gameObject; 		//instantiate the broken asset to the second child of the object with this script
		explosionLocation = this.transform.position;				//instantiate the location of the explosion to the current object's location: TEMPORARY

		foreach (Transform child in brokenObject.transform) 		//loop through all the shards (the children of the broken object) and deactivate them in the scene
		{
			child.gameObject.SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.H))								//When H is pressed: TEMPORARY
		{
			completeObject.SetActive(false);						//deactivate the complete asset from the scene

			foreach (Transform child in brokenObject.transform) 	//loop through all the shards (the children of the broken object), 
			{														//	reactivate them, and apply an explosive for, coming from the explosion center.
				child.gameObject.SetActive(true);
				child.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionLocation, explosionSize);
			}

		}
		
	}
}
