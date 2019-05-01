using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiVision : MonoBehaviour
{
    public bool opponentSpotted;
    public GameObject enemySeen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnTriggerEnter(Collider other){
     if (other.tag == "Player"){
          /*add an if statement here that makes sure that the enemy doesn't see things that are behind it, 
          which is way easier and less resource intensive than making a custom half circle mesh for the collider*/
          opponentSpotted = true;
          enemySeen = other.gameObject;
     }
    }
   private void OnTriggerExit(Collider other)
     {
          if (other.tag == "Player"){
               opponentSpotted = false;
               enemySeen = null;
          }
     }
}
