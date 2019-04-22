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
