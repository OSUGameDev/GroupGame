using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
     // Start is called before the first frame update
    public bool targetFound;
    public GameObject currentTarget;
    public GameObject aiVision;
    public GameObject laserBolt;
    public float lookSpeed = 3;
    public float reloadSpeed = 2; //seconds
    private float nextFire;

  //


    void Start(){
        aiVision = this.gameObject.transform.GetChild(0).gameObject;//get the Enemy Vision Object
        nextFire = Time.time + 5; //set enemy weapon to start unloaded
        
    }

    // Update is called once per frame

    void Update(){
        
        lookForTarget();
        if(targetFound){ //engages target as long as targetFound is true, it is important to make sure that the enemy does not try to engage with a null gameobject since that will cause a crash
            engageTarget();
        }
        else
            nextFire = Time.time + 4; // allow time for the enemy to rotate before firing since it'll shoot while turning
    }



    void lookForTarget(){ //Checks vision child for if it sees an enemy
          targetFound = aiVision.GetComponent<EnemyAiVision>().opponentSpotted;
        if(targetFound)
            currentTarget = aiVision.GetComponent<EnemyAiVision>().enemySeen;
    }
    void engageTarget(){ //for not engaging the target will simply be rotating at it
        Vector3 targetDir = currentTarget.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = lookSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);

        shootTarget(); //it's pew pew time
    }
    void shootTarget(){
        if(Time.time > nextFire){
            var newLaser = Instantiate (laserBolt,transform); //laserbolt will get the location of the current target on it's start
            newLaser.transform.parent = null; //sets the enemy as the parent of the laser so we can give the location of the current target easier
            //newLaser.transform.LookAt(currentTarget.transform.position);
            nextFire = reloadSpeed + Time.time; // set next time to fire to be whatever the reload speed is 
        }   
    }
    
}


