using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun {
    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

    protected override void FireProjectile() {
        base.FireProjectile();
    }

    //this function is unity one that sets the variables in the unity editor to this by default
    public void Reset() {
        coolDownMS = 250f;
    }
}
