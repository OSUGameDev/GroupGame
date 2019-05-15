using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet {

    public float maxLength = 5000f;

	private void Reset() {
		damage = 50;
		explosive = false;
		maxExistTime = 2f;
		speed = 0;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
        transform.Rotate(90, 0, 0);

        float laserLength = 0;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxLength)) {
            laserLength = Vector3.Distance(transform.position, hit.point);
            if(laserLength > maxLength) {
                laserLength = maxLength;
            }
        } else {
            laserLength = maxLength;
        }

        transform.localScale = new Vector3(transform.localScale.x, laserLength, transform.localScale.z);
        transform.position += transform.forward * laserLength/2f;
	}


	public override void OnHit(Collider obj) {
		if(obj.tag == "Target Tester" || obj.tag == "Enemy") {
			Destruct();
		}
	}

}
