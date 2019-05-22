using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRocket : Bullet {

    public override void Reset() {
        base.Reset();

        damage = 50;
        explosive = true;
        maxExistTime = 10f;
        speed = 10f;
    }

	// Use this for initialization
	protected override void Start (){
        base.Start();
    }

    public new void Destruct() {
        explode();

        this.gameObject.SetActive(false);
    }

    public override void OnHit(Collider obj) {
        if(obj.name == "Rocket Launcher") { //don't want it to collide with the rocket launcher itself.
            return;
        }

        Destruct();
    }
}
