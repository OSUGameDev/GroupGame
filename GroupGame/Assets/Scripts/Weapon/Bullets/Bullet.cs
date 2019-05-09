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
    public bool bouncy = false;
    public bool usesGravity = false;

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

        gameObject.name = this.GetType().Name; //will need to edit tag later to include playerID to allow collisions with other bullets.
        gameObject.GetComponent<Rigidbody>().velocity *= speed;
    }

    protected virtual void Update() {
		existTime += Time.deltaTime; //counting time based on seconds since last frame

		//deleting object if it exceeds it's maximum lifespan
		if (existTime >= maxExistTime) {
			Destruct();        
		}
    }

    private Vector3 oldVelocity;
    protected virtual void FixedUpdate() {
        oldVelocity = gameObject.GetComponent<Rigidbody>().velocity;
    }

    //Called on hit with anything. 
    public abstract void onHit(Collider obj);

    public virtual void Destruct() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider) {
        //automatically preventing bullets from coliding with the player or other bullets of the same type. 
        if (collider.gameObject.name == "Player" || collider.gameObject.name == this.GetType().Name) {
            return;
        }

        //TODO: talk about implications of having a rigidbody world
        if (bouncy) { //creating bouncy objects is difficult without world objects having a rigidbody.
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                //calculating reflection
                Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, hit.normal);
                gameObject.GetComponent<Rigidbody>().velocity = reflectedVelocity;

                //rotating object.
                Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
                transform.rotation = rotation * transform.rotation;
            }
        }
        
        onHit(collider);
    }

}
