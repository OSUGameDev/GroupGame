using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet {

    public float maxLength = 5000f;

    public override void Reset() {
        base.Reset();

        damage = 50;
		explosive = false;
		maxExistTime = 2f;
		speed = 0;

        FixLaser();
    }

    private void FixLaser() {
        transform.Rotate(90, 0, 0); //needs to be here it won't reload on 
        float laserLength = 0;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLength, BULLET_IGNORE_LAYER)) {
            laserLength = Vector3.Distance(transform.position, hit.point);
            if (laserLength > maxLength) {
                laserLength = maxLength;
            }
        } else {
            laserLength = maxLength;
        }

        transform.localScale = new Vector3(transform.localScale.x, laserLength, transform.localScale.z);
        transform.rotation = Quaternion.LookRotation((hit.point - transform.position).normalized);

        transform.Translate(transform.forward * laserLength/2, Space.World);
    }

	// Use this for initialization
	protected override void Start () {
		base.Start();

        
        
	}


	public override void OnHit(Collider obj) {
		if(obj.tag == "Target Tester" || obj.tag == "Enemy") {
			Destruct();
		}
	}

}
