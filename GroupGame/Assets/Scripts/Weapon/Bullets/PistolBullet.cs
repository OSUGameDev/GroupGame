using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : Bullet {

    public override void onHit(Collider obj) {
        if(obj.tag != "Player") {//hoping the player actually has this as it's tag...
            if(obj.tag == "Target Tester") {//doesn't actually work all the time?
                obj.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    protected override void Start() {
        this.speed = 100f;
        this.damage = 20;
        base.Start();
    }

}
