using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    /********** Parameters **********/

    public Transform target;
    public float rotate_speed;

    private float MouseX, MouseY;
    private Quaternion rotationStart, rotationEnd;
    private bool isAim, isLock;
    private Camera cam;
    private GameObject lock_target;

    /********** Functions **********/


    private void rotate()
    {
        //This part sets the camera's local root position
        if(!isAim)
        {
            //Change the camera base(local) position to normal 3rd person position
            this.transform.localPosition = new Vector3(0f, 1.0f, -2.5f);
        }
        else
        {
            //Change the camera base(local) position to shoulder position
            this.transform.localPosition = new Vector3(-0.8f, 0.75f, -1.2f);
        }

        //This part is the camera rotate part
        {
            this.MouseX += Input.GetAxis("Mouse X") * this.rotate_speed * Time.deltaTime;
            this.MouseY -= Input.GetAxis("Mouse Y") * this.rotate_speed * Time.deltaTime;
            this.MouseY = Mathf.Clamp(this.MouseY, -35, 60);
            //Get the start position, which is the current position
            rotationStart = transform.rotation;
            //Find the target rotation which we're going to
            rotationEnd = Quaternion.Euler(this.MouseY, this.MouseX, 0f);
            this.target.rotation = Quaternion.Lerp(rotationStart, rotationEnd, 1.0f);
        }

        //This is the looking at part
        if (!isAim)
        {
            //Set the camera to focus on the player
            this.transform.LookAt(this.target);
        }
        else
        {

            Transform player = this.transform.parent.transform.parent;
            Quaternion start, end;
            start = player.rotation;
            end = Quaternion.Euler(0f, this.MouseX, 0f);
            player.rotation = Quaternion.Lerp(start, end, 1.0f);
        }
    }

    public float getAngle()
    {
        return this.MouseX;
    }

    public void lockOn()
    {
        //Set the shoulder view
        this.transform.localPosition = new Vector3(-0.8f, 0.75f, -1.2f);
        this.transform.LookAt(lock_target.transform.position);

    }

    /********** Build in Functions **********/

    // Use this for initialization
    void Start ()
    {
        isAim = false;
        isLock = false;
        cam = GetComponent<Camera>();
	}

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            isAim = true;
        }
        else
        {
            isAim = false;
        }

        if(isAim)
        {
            if(Input.GetButtonDown("LockOn"))
            {
                Debug.Log("Lock!");
                isLock = true;
            }
        }
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if(!isLock)
        {
            rotate();
        }
        else
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Target Tester")
                {
                    lock_target = hit.collider.gameObject;
                }
            }
            lockOn();
        }
	}
}
