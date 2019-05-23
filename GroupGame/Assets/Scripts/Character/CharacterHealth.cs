using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour {

    /***** Variables *****/
    public int Current_Health;          //This is the player's current health
    public int Start_Health = 100;      //This is the player's start health
    public Slider Health_Slider;


    /***** Flags *****/
    bool isDead;
    bool isDamaged;


    /***** Referenced Elements *****/
    Animator Anim;                      //Reference to the player's animator
    CharacterMovement Ch_Move;          //Reference to the player's movement script


    //This is a unity built-in function.
    //This function will be called the first time this script been loaded into the scene.
    void Awake()
    {
        Current_Health = Start_Health;      //Initialize the player's health

        Anim = GetComponent<Animator>();        //Get the component
        Ch_Move = GetComponent<CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        isDamaged = false;      //Reset the flag
	}


    //This function will be called when the player take damage from a specific source
    public void TakeDamage(int amount)
    {
        isDamaged = true;       //Set the flag

        Current_Health -= amount;       //Reduce the health

        Health_Slider.value = Current_Health;       //Update the slider

        if(Current_Health <= 0 && !isDead)          //Check if the player dead
        {
            Death();        //Call the death function
        }
    }


    //This function will be called when the player dead
    void Death()
    {
        isDead = true;      //Set the death flag so this function won't be called again.

        Ch_Move.enabled = false;        //Turn off the movement and shooting scripts.


    }


    //This is a unity built-in function
    //This function will be called when the current object hit another trigger object's collider
    //The parameter "other" will contain the target object's information
    private void OnTriggerEnter(Collider other)
    {
        //This is only for testing
        if(other.gameObject.tag == "Death Cube")
        {
            TakeDamage(20);       //If collect the death cube, take 20 damage
            other.gameObject.SetActive(false);      //Deactive the cube
        }
    }
}
