using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunExtend : Gun {
    
    // Use this for initialization
    public override void Start () {
        base.Start();
	}

	//this function is unity one that sets the variables in the unity editor to this by default
	public void Reset() {
        base.Reset();
		coolDownMS = 1000f;
	}
    
}