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
    private bool isAim, isLock, isMelee;
    private Camera cam;
    private GameObject lock_target;

    /********** Functions **********/


    private void rotate()
    {
        /*****Camera's look at position*****/
        if (!isAim && !isMelee)
        {
            //Set the camera to focus on the player,
            //this should only affect the normal mode (neither melee nor aim)
            transform.LookAt(target);
        }
        else if(isLock)
        {
            //Look at the target
            transform.LookAt(lock_target.transform.position);
            Vector3 dir = lock_target.transform.position - player.position;
            player.forward = new Vector3(dir.x, 0f, dir.z);
        }
        else
        {
            //If in melee or aim mode, rotate the player with camera
            Quaternion start, end;
            start = player.rotation;
            end = Quaternion.Euler(0f, MouseX, 0f);
            player.rotation = Quaternion.Lerp(start, end, 1.0f);
        }

        /*****Camera's rotation*****/
        if(!isLock)
        {
            //Get the user rotation input
            MouseX += Input.GetAxis("Mouse X") * rotate_speed * Time.deltaTime;
            MouseY -= Input.GetAxis("Mouse Y") * rotate_speed * Time.deltaTime;
            MouseY = Mathf.Clamp(MouseY, -90, 25);
            //Get the start position, which is the current position
            rotationStart = transform.rotation;
            //Find the target rotation which we're going to
            rotationEnd = Quaternion.Euler(MouseY, MouseX, 0f);
            //Smooth rotate the camera
            target.rotation = Quaternion.Lerp(rotationStart, rotationEnd, 0.5f);
        }


        /*****Camera's local root position*****/
        if (!isAim && !isMelee)
        {
            //Debug.Log("Normal!");
            //Debug.Log(isLock);
            //Change the camera base(local) position to normal 3rd person position
            transform.localPosition = new Vector3(0f, 1.0f, -2.5f);
        }
        else
        {
            //Debug.Log("Shoulder!");
            //Debug.Log(isLock);
            //Change the camera base(local) position to shoulder position
            transform.localPosition = new Vector3(-0.8f, 0.75f, -1.2f);
        }
    }

    public float getAngle()
    {
        return MouseX;
    }

    public Transform getTarget()
    {
        return lock_target.transform;
    }

    public bool checkLock()
    {
        return isLock;
    }

    /********** Build in Functions **********/

    // Use this for initialization
    void Start ()
    {
        isAim = false;
        isLock = false;
        isMelee = false;
        cam = GetComponent<Camera>();
        player_script = player.GetComponent<CharacterMovement>();
	}

    void Update()
    {
        //Check if the player enter aim mode
        if(Input.GetMouseButton(1))
        {
            //Change the flag
            isAim = true;
            //Change player's animation
            player_script.setAim(true);
        }
        else
        {
            isAim = false;
        }
        player_script.setAim(isAim);

        //Check if player enter melee mode
        if (Input.GetButtonDown("Melee"))
        {
            //Change the flag
            isMelee = !isMelee;
        }
        player_script.setMelee(isMelee);

        //Check if player enter the lock mode
        if (isMelee)
        {
            //Only the melee mode could lock on target
            if(Input.GetButtonDown("LockOn"))
            {
                //Change the flag
                isLock = !isLock;
                if(isLock)      //If enter the lock mode
                {
                    //Find the target
                    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Target Tester")
                        {
                            lock_target = hit.collider.gameObject;
                        }
                    }
                }
                else    //If leave the lock mode
                {
                    //In case the camera's z rotation has change, set it to 0
                    transform.localRotation = Quaternion.Euler(21.801f, 0f, 0f);
                }
            }
        }
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        rotate();
	}
}
