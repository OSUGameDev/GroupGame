using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet {

	private void Reset() {
		damage = 50;
		explosive = false;
		maxExistTime = 2f;
		speed = 10f;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}


	public override void onHit(Collider obj) {
		if(obj.tag == "Target Tester" || obj.tag == "Enemy") {
			Destruct();
		}
	}

}
