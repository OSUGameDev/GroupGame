using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour {

    private int current_view;       //Represents of the current camera view, 1 for first person, 3 for 3rd person

	// Use this for initialization
	void Start ()
    {
        SwitchView(1);      //Initialize with first view
	}
	
	// Update is called once per frame
	void Update ()
    {
        //If the player press the view switch button
		if(Input.GetButtonDown("SwitchView"))
        {
            if(current_view == 1)       //If current is FPV
            {
                SwitchView(3);
            }
            else if(current_view == 3)  //If current is TPV
            {
                SwitchView(1);
            }
            else
            {
                Debug.Log("== Error! View mode undefined!");
            }
        }
	}


    //This function was used to switch the current view mode between FPV and TPV
    private void SwitchView(int mode)
    {
        if(mode == 1)
        {
            current_view = 1;       //Set the flag
            transform.GetChild(1).gameObject.SetActive(false);       //Disable the 3rd view
            transform.GetChild(0).gameObject.SetActive(true);       //Enable the 1st view
        }
        else if(mode == 3)
        {
            current_view = 3;       //Set the flag
            transform.GetChild(0).gameObject.SetActive(false);       //Disable the 1st view
            transform.GetChild(1).gameObject.SetActive(true);       //Enable the 3rd view
        }
    }
}
