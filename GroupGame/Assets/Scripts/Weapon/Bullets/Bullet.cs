using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Bullet : MonoBehaviour {

//IDENTIFICATION VARS
    //TODO: Move this to some static class that contains info like this. 
    //or maybe create some function that handles layer numbers using hashmaps or something
    public const byte BULLET_IGNORE_LAYER = 17;
	private const float RAYCAST_RATIO = 0.45f / 20f; //a decent ratio used to linearly calucalate how far the bounce raycast should scan.

//BASIC VARS 

    //use Reset() to set these in child classes!
    public int damage = 0;
    public float speed = 10f;
    protected float existTime = 0f;
    public float maxExistTime = 4f;
    //TODO: bullet dropoff? 

//BOUNCE VARS
    public bool bouncy = false;
    public int maxBounces = 12;
    protected int currentBounces = 0;
	private float raycastDistance = 0; //how far it scans for an objcet to bounce off of


//EXPLOSION VARS
    protected PooledGameObjects pgo;
    protected int explosionId;

    public bool explosive = false;
    public float explosionRadius = 0;
    public GameObject explosionObj; //default explosion loaded if one not provided, just ensure to call base.Start() in function override 

 

    // Please call this using base.Start in your override. 
    protected virtual void Start () {
        gameObject.layer = BULLET_IGNORE_LAYER;
        gameObject.name = this.GetType().Name; //will need to edit tag later to include playerID to allow collisions with other bullets.

        ;

        //making sure not to overwrite manually targeted explosion
        if (explosive) {
            if (explosionObj == null) {
                //currently doesn't work, please drag explosion manually
                explosionObj = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/Effect/Explosion/Explosion", typeof(GameObject));
            }

            pgo = GameObject.Find("PooledBullets").GetComponent<PooledGameObjects>();
            explosionId = pgo.InitializeObjectType(explosionObj);
        }
    }

    /// <summary>
    /// This is used by the Unity Editor to set the UI to match these vars, useful when you want the child class to have different default values for these things.
    /// Use this when you want to reset the bullet. 
    /// </summary>
    public virtual void Reset() {
        existTime = 0;
        currentBounces = 0;

        //default velocity is set by gun class using the camera.
        gameObject.GetComponent<Rigidbody>().velocity *= speed;

        if (bouncy) {//fixes issue where gun shoots bullet next to a surface.
            CheckBounce();
        }

		raycastDistance = RAYCAST_RATIO * speed;
    }

    protected virtual void Update() {
		existTime += Time.deltaTime; //counting time based on seconds since last frame

		//deleting object if it exceeds it's maximum lifespan
		if (existTime >= maxExistTime) {
			Destruct();        
		}
    }

    private Vector3 oldVelocity; //this is only used with bullet bounces.
    protected virtual void FixedUpdate() {
        if (bouncy) { //needs to run here because normal collision sucks
            oldVelocity = gameObject.GetComponent<Rigidbody>().velocity;
            CheckBounce();
        }
    }

    //Called on hit with anything. 
    public abstract void OnHit(Collider obj);

    public void Destruct() {
        if (explosive) {
            explode();
        }

        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider) {
        //automatically preventing bullets from coliding with the player or other bullets of the same type. 
        if (collider.gameObject.name == "Player" || collider.gameObject.name == this.GetType().Name) {
            return;
        }else if (collider.tag == "Target Tester" || collider.tag == "Enemy") { //only explodes if the bullet hits a valid enemy. 
            if (explosive) { //calls explosion if 
                explode();
            }
        }
        
        OnHit(collider);
    }

    /// <summary>
    /// Collision detection is to slow to accurately reflect bullets. So this is called in fixed update and raycasts x amt in front of the bullet.
    /// Currently has issues with bullet to fast. 
    /// </summary>
    private void CheckBounce() {
        RaycastHit hit;

        //TODO: make max distance based on the speed of the bullet. Alse the bullet should be moved to the object it's bouncing off of, then reflected (will look much better). 
		if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, BULLET_IGNORE_LAYER)) {
            if (!hit.collider) {//don't bounce if object is not a collider.
                return;
            }

            //calculating reflection
            Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, hit.normal);
            gameObject.GetComponent<Rigidbody>().velocity = reflectedVelocity;

            //rotating object to reflection direction.
            Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
            transform.rotation = rotation * transform.rotation;

			this.transform.position = hit.point;

            currentBounces++;
            if(currentBounces > maxBounces) {
                Destruct();
            }

			CheckBounce ();
        }
    }

    protected void explode() {
        if (explosionObj != null) { //calls explosion if 
            GameObject exp = pgo.GetPooledObject(explosionId);
            exp.transform.position = transform.position;
            exp.transform.rotation = transform.rotation;
            exp.GetComponent<Explosion>().Reset(); //very important!!!

            exp.SetActive(true);
        } else {
            Debug.Log("Explosion not defined for this object: " + this.name);
        }
    }

}
