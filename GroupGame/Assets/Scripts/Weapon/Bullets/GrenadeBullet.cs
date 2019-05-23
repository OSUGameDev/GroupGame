using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : Bullet {

    public override void Reset() {
        base.Reset();

        damage = 50;
        explosive = true;
        maxExistTime = 2f;
        speed = 30f;
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
