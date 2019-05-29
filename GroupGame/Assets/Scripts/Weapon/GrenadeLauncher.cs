using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Gun {


    protected override void FireProjectile() {
        base.FireProjectile();
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
        Debug.Log("Gange");
	}

}
