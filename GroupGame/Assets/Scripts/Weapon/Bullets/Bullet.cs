using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Bullet : MonoBehaviour {

    //use Reset() to set these in child classes!
    public int damage = 0;
    public float speed = 10f;
    public float maxExistTime = 10f;
    //TODO: bullet dropoff? 

    protected float existTime = 0f;
    

//EXPLOSION VARS
    public bool explosive = false;
    public float explosionRadius = 0;
    public GameObject explosion; //default explosion loaded if one not provided, just ensure to call base.Start() in function override 

    
    // Use this for initialization
    protected virtual void Start () {
        //making sure not to overwrite manually targeted explosion
        if (explosive && explosion == null) {
            //currently doesn't work, please drag manually
            explosion = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/Effect/Explosion/Explosion", typeof(GameObject));
        }

        gameObject.GetComponent<Rigidbody>().velocity *= speed;
    }

    protected virtual void Update() {
        
    }

    protected virtual void FixedUpdate() {
        existTime += Time.deltaTime; //counting time based on seconds since last frame

        //deleting object if it exceeds it's maximum lifespan
        if (existTime >= maxExistTime) {
            Destruct();        
        }
    }
    
    //Called on hit with anything. 
    public abstract void onHit(Collider obj);

    public virtual void Destruct() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider obj) {
        //automatically preventing bullets from coliding with the player
        if (obj.tag == "Player") {//hoping the player actually has this as it's tag...
            return;
        }
        onHit(obj);
    }

}
