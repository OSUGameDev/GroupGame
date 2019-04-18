using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour {

    //set these in child classes!
    public int damage = 0;
    public float speed = 0;

    //TODO: bullet dropoff? 

    public Rigidbody bulletModel;

    // Use this for initialization
    protected virtual void Start () {
        bulletModel.velocity *= speed;
    }

    void FixedUpdate() {
        
    }

    //<summary>
    //Called on hit with anything. 
    //</summary>
    public abstract void onHit(Collider obj);

    private void OnTriggerEnter(Collider obj) {
        onHit(obj);
    }

}
