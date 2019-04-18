using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Screen.SetResolution(1920, 1200, true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
