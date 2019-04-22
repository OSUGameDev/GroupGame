using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
     // Start is called before the first frame update
    public bool targetFound;
    private GameObject currentTarget;
    public GameObject aiVision;
    public float lookSpeed = 3;
  

    void Start(){
        aiVision = this.gameObject.transform.GetChild(0).gameObject;//get the Enemy Vision Object
    }

    // Update is called once per frame

    void Update(){
        lookForTarget();
        if(targetFound){ //engages target as long as targetFound is true, it is important to make sure that the enemy does not try to engage with a null gameobject since that will cause a crash
            engageTarget();
        }
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
    }
}
