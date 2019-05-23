using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : Bullet {

    public void Reset() {
        base.Reset();
        damage = 50;
        explosive = true;
        maxExistTime = 2f;
        speed = 10f;
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

    public override void Destruct() {
        Instantiate(explosion, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
