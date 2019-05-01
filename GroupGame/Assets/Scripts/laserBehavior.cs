using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject target;
    private Vector3 fireTowards; //snapshot of location //apparently doing Vector3 instead of Transform will dereference the position instead of creating a pointer, which is good since we dont
    //want the position to follow the player's movement
    public float flightSpeed = 2;
    public float decayTime = 1.9f; //time it takes for the laser to dissapate, should be less than reload speed to prevent spooky conflicts probably
    private float timer = 0;

    public float fireDistance = 50;
    void Start()
    {
        target = transform.parent.gameObject.GetComponent<EnemyAI>().currentTarget;
        fireTowards = target.transform.position;
        transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.position += transform.forward * Time.deltaTime * flightSpeed ;
        if(timer > decayTime ) //if decaytime has passed then delet this object
            Destroy(gameObject); //rip 

    }
}
