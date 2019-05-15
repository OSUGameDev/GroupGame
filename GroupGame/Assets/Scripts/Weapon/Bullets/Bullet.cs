using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Bullet : MonoBehaviour {

    //TODO: Move this to some static class that contains info like this. 
    //or maybe create some function that handles layer numbers using hashmaps or something
    protected const byte BULLET_IGNORE_LAYER = 17;
    protected float existTime = 0f;

//BASIC VARS 

    //use Reset() to set these in child classes!
    public int damage = 0;
    public float speed = 10f;
    public float maxExistTime = 10f;
    //TODO: bullet dropoff? 
    
    public bool usesGravity = false;

//BOUNCE VARS
    public bool bouncy = false;
    public int maxBounces = 2;
    private int currentBounces = 0;

    




//EXPLOSION VARS
    public bool explosive = false;
    public float explosionRadius = 0;
    public GameObject explosion; //default explosion loaded if one not provided, just ensure to call base.Start() in function override 

    
    // Use this for initialization
    protected virtual void Start () {
        //making sure not to overwrite manually targeted explosion
        if (explosive && explosion == null) {
            //currently doesn't work, please drag explosion manually
            explosion = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/Effect/Explosion/Explosion", typeof(GameObject));
        }

        gameObject.name = this.GetType().Name; //will need to edit tag later to include playerID to allow collisions with other bullets.
        gameObject.GetComponent<Rigidbody>().velocity *= speed;
        gameObject.layer = BULLET_IGNORE_LAYER;

        if (bouncy) {//fixes issue where bouncy can 
            CheckBounce();
        }
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
        if (bouncy) { //needs to run here because normal collision sucks
            oldVelocity = gameObject.GetComponent<Rigidbody>().velocity; //this is only used with bullet bounces.
            CheckBounce();
        }
    }

    //Called on hit with anything. 
    public abstract void OnHit(Collider obj);

    public virtual void Destruct() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider) {
        //automatically preventing bullets from coliding with the player or other bullets of the same type. 
        if (collider.gameObject.name == "Player" || collider.gameObject.name == this.GetType().Name) {
            return;
        }
        
        OnHit(collider);
    }

    /// <summary>
    /// Collision detection is to slow to accurately reflect bullets. So this is called in fixed update and raycasts x amt in front of the bullet.
    /// Currently has issues with bullet to fast. 
    /// </summary>
    private void CheckBounce() { //TODO: Add code to 'backpedal' bullet to check if it already passed something it should've bounced on.
        RaycastHit hit;

        //raycast ignores the 'bulletIgnore' layer, only checks within 0.5f distance in front of the bullet
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.35f, BULLET_IGNORE_LAYER)) {
            if (!hit.collider) {//don't bounce if object is not a collider.
                return;
            }

            //calculating reflection
            Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, hit.normal);
            gameObject.GetComponent<Rigidbody>().velocity = reflectedVelocity;

            //rotating object to reflection direction.
            Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
            transform.rotation = rotation * transform.rotation;

            currentBounces++;
            if(currentBounces > maxBounces) {
                Destruct();
            }
        }
    }

}
