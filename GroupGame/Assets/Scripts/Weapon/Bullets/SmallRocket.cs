using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRocket : MonoBehaviour {

    private float life_time = 0.0f;
    private float max_life = 5.0f;
    private Collider physical_collider;
    public float power = 100.0f;       //The explosion power
    public float explosion_radius = 20.0f;      //The explosion radius


	// Use this for initialization
	void Start () {
        physical_collider = GetComponent<CapsuleCollider>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        life_time += Time.deltaTime;
        if(life_time >= max_life)
        {
            Destroy(gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(life_time >= 0.05f)
        {
            Collider[] explod_list = Physics.OverlapSphere(transform.position, explosion_radius);
            foreach (Collider explod_object in explod_list)
            {
                if (explod_object.gameObject.GetComponent<Rigidbody>())
                {
                    explod_object.gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, explosion_radius);
                }
            }
            Destroy(gameObject);
        }
    }
}
