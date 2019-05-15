using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet {

    private void Reset() {
        speed = 20f;
        damage = 20;

        explosive = false;
        maxExistTime = 10f;
    }

    public override void OnHit(Collider obj) {
        if (obj.tag == "Target Tester") {//doesn't actually work all the time?
            obj.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destruct();
        }
        
    }

    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
    

}
