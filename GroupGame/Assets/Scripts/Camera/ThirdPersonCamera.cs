using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    /********** Parameters **********/

    public Transform target;
    public float rotate_speed;

    private float MouseX, MouseY;

    /********** Functions **********/

    private void rotate()
    {
        this.MouseX += Input.GetAxis("Mouse X") * this.rotate_speed;
        this.MouseY -= Input.GetAxis("Mouse Y");
        this.MouseY = Mathf.Clamp(this.MouseY, -35, 60);

        this.target.rotation = Quaternion.Euler(this.MouseY, this.MouseX, 0f);
        this.transform.LookAt(this.target);
    }



    public float getAngle()
    {
        return this.MouseX;
    }

    /********** Build in Functions **********/

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
            rotate();
	}
}
