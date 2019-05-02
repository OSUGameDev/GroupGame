using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void onTriggerEnter(Collider other){
		Debug.Log ("Are you Enemy?");
		if (other.tag == "Enemy") {
			Debug.Log ("Enemy Spotted");
			other.transform.localScale += new Vector3 (0.5f, 0.5f, 0.5f);
		}
	}
}
