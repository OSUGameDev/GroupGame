using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun
{

    public new void Reset() {
        base.Reset();
        coolDownMS = 1000f;
    }
    
    protected override void FireProjectile() {
        base.FireProjectile();
    }

    // Use this for initialization
    public override void Start() {
        base.Start();
    }
    
}
