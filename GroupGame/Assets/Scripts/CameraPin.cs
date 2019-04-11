using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPin : MonoBehaviour {

	public Camera playerCamera;

	// Use this for initialization
	void Start () {
		if (!playerCamera)
			return;
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = playerCamera.transform.position;
	}
}
