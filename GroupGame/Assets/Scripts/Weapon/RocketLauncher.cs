using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun
{

    public void Reset() {
        coolDownMS = 1000f;
}

    protected override void FireProjectile() {
        base.FireProjectile();
    }

    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
    
}
