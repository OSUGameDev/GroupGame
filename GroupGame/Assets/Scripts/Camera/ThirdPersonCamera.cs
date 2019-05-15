using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    /********** Parameters **********/

    public Transform target, player;
    public float rotate_speed;

    private CharacterMovement player_script;
    private float MouseX, MouseY;
    private Quaternion rotationStart, rotationEnd;
    private bool isAim, isLock;
    private Camera cam;
    private GameObject lock_target;

    /********** Functions **********/


    private void rotate()
    {
        /*****Camera's look at position*****/
        if (!isAim)
        {
            //Set the camera to focus on the player
            this.transform.LookAt(this.target);
        }
        else
        {
            Quaternion start, end;
            start = this.player.rotation;
            end = Quaternion.Euler(0f, this.MouseX, 0f);
            this.player.rotation = Quaternion.Lerp(start, end, 1.0f);
        }

        /*****Camera's rotation*****/
        {
            //Get the user rotation input
            this.MouseX += Input.GetAxis("Mouse X") * this.rotate_speed * Time.deltaTime;
            this.MouseY -= Input.GetAxis("Mouse Y") * this.rotate_speed * Time.deltaTime;
            this.MouseY = Mathf.Clamp(this.MouseY, -35, 60);
            //Get the start position, which is the current position
            rotationStart = transform.rotation;
            //Find the target rotation which we're going to
            rotationEnd = Quaternion.Euler(this.MouseY, this.MouseX, 0f);
            //Smooth rotate the camera
            this.target.rotation = Quaternion.Lerp(rotationStart, rotationEnd, 0.5f);
        }


        /*****Camera's local root position*****/
        if (!isAim)
        {
            //Change the camera base(local) position to normal 3rd person position
            this.transform.localPosition = new Vector3(0f, 1.0f, -2.5f);
        }
        else
        {
            //Change the camera base(local) position to shoulder position
            this.transform.localPosition = new Vector3(-0.8f, 0.75f, -1.2f);
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
        player_script = player.GetComponent<CharacterMovement>();
	}

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            this.isAim = true;
        }
        else
        {
            this.isAim = false;
        }
        this.player_script.setAim(this.isAim);

        if (isAim)
        {
            if(Input.GetButtonDown("LockOn"))
            {
                Debug.Log("Lock!");
                isLock = true;
                isAim = true;
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
