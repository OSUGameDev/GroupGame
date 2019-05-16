using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : Bullet {

	private int explosionId;
	private PooledGameObjects pgo;

    public void Reset() {
        base.Reset();
        damage = 50;
        explosive = true;
        maxExistTime = 2f;
        speed = 30f;
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();
		pgo = GameObject.Find("PooledBullets").GetComponent<PooledGameObjects>();
		explosionId = pgo.InitializeObjectType(explosion);
	}
	

    public override void OnHit(Collider obj) {
        if(obj.tag == "Target Tester" || obj.tag == "Enemy") {
            Destruct();
        }
    }

    public override void Destruct() {
		GameObject exp = pgo.GetPooledObject(explosionId);
		exp.transform.position = transform.position;
		exp.transform.rotation = transform.rotation;
		exp.GetComponent<Explosion>().Reset();
		exp.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
