using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    /***** Variables *****/
    public int Current_Health;          //This is the player's current health
    public int Start_Health = 100;      //This is the player's start health
    public Slider Health_Slider;


    /***** Flags *****/
    bool isDead;
    bool isDamaged;


    /***** Referenced Elements *****/
    Animator Anim;                      //Reference to the player's animator


    //This is a unity built-in function.
    //This function will be called the first time this script been loaded into the scene.
    void Awake()
    {
        if(gameObject != null) {
            gameObject.layer = Bullet.BULLET_IGNORE_LAYER; //prevents bouncy bullets from bouncing off damagable objects, should be updated when bouncy code changes. 
        }

        Current_Health = Start_Health;      //Initialize the player's health

        Anim = GetComponent<Animator>();        //Get the component
    }

    // Update is called once per frame
    void Update()
    {
        isDamaged = false;      //Reset the flag
    }


    //This function will be called when the player takes damage from a specific source
    public void TakeDamage(int amount)
    {
        Debug.Log("Being hit!");

        isDamaged = true;       //Set the flag

        Current_Health -= amount;       //Reduce the health

        if (Current_Health <= 0 && !isDead)          //Check if the player dead
        {
            Death();        //Call the death function
        }
    }


    //This function will be called when the player dead
    void Death()
    {
        Debug.Log("Enemy Dead!");
        isDead = true;      //Set the death flag so this function won't be called again.

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }


    //This is a unity built-in function
    //This function will be called when the current object hit another trigger object's collider
    //The parameter "other" will contain the target object's information
    private void OnTriggerEnter(Collider other)
    {
        //This is only for testing
        if (other.gameObject.tag == "Death Cube")
        {
            TakeDamage(20);       //If collect the death cube, take 20 damage
            other.gameObject.SetActive(false);      //Deactive the cube
        }
    }
}
