﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun {
    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    protected override void FireProjectile() {
        base.FireProjectile();
    }

    //this function is unity one that sets the variables in the unity editor to this by default
    public new void Reset() {
        base.Reset();
        coolDownMS = 250f;
    }
}
