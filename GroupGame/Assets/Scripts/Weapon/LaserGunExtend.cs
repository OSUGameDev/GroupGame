using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunExtend : Gun {

    private LineRenderer laser;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

	//this function is unity one that sets the variables in the unity editor to this by default
	public void Reset() {
		coolDownMS = 400f;
	}
    
}