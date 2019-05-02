using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {
	public Collider lightAttack;
	public Collider heavyAttack;
	public bool isMeleeEquipped = true;
	public float lightAttackDelay = 0.0f;
	public float heavyAttackDelay = 0.5f;
	public float lightAttackDuration = 0.5f;
	public float heavyAttackDuration = 1.0f;

	// Use this for initialization
	void Start () {
		this.lightAttack.enabled = false;
		this.heavyAttack.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMeleeEquipped) {
			if (Input.GetMouseButtonDown(0)) {
				StartCoroutine (HandleLightMelee ());
			}

			if (Input.GetMouseButtonDown (1)) {
				StartCoroutine (HandleHeavyMelee ());
			}
		}
	}

	IEnumerator HandleHeavyMelee() {
		Debug.Log ("Heavy Attack Started");
		yield return new WaitForSeconds(heavyAttackDelay);
		this.heavyAttack.enabled = true;
		yield return new WaitForSeconds(heavyAttackDuration);
		this.heavyAttack.enabled = false;
		Debug.Log ("Heavy Attack Ended");
	}

	IEnumerator HandleLightMelee() {
		Debug.Log ("Light Attack Started");
		yield return new WaitForSeconds(lightAttackDelay);
		this.lightAttack.enabled = true;
		yield return new WaitForSeconds(lightAttackDuration);
		this.lightAttack.enabled = false;
		Debug.Log ("Light Attack Ended");
	}
}
