using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour
{
    public float ispeed, speed = 2.0F;          //the moving speed of the character
    public float jumpSpeed = 10.0f;      //the jump force of the character

    public float gravity = 20.0f;       //the force of gravity on the character

    public float GroundOffset = .2f;    //the offset for the IsGrounded check. Useful for recognizing slopes and imperfect ground.
    public GameObject Sword, RightHand;
    public GameObject particle1, particle2;

    private float moveH, moveV;
    private Vector3 moveDirection = Vector3.zero;   //the direction the character should move.
    private Vector3 jumpDirection = Vector3.zero;
    private bool isMove, isRun, isAim, isMelee, swordMove;

    private CharacterController controller;
    private Animator anim;

    private ThirdPersonCamera cam;

    private Transform sword;


    ///The check to see if the character is currently on the ground.
    private bool isGrounded()
    {
        RaycastHit hit;
        Physics.Raycast(this.transform.position, -this.transform.up, out hit, 10);   //A short ray shot directly downward from the center of the character.

        if (System.Math.Abs(hit.distance) < System.Single.Epsilon)                                           //if the distance is zero, the ray probably did not hit anything.
        {
            return false;
        }
        if (hit.distance <= (this.transform.lossyScale.y / 2 + GroundOffset))   //if the distance from the ray is less than half the height 
        {                                                                   //of the character (plus the offset), the character us grounded.
            return true;
        }
        return false;
    }

    private void checkMove()
    {
        if (!isMove)
            speed = ispeed;
    }

    private void move()
    {
        /*****Get basic player input*****/  
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        if (moveH != 0 || moveV != 0)
        {
            isMove = true;
            //Once the player start move, the forward direction should set same with camera.
            //In other word, the mouse should now be able to rotate the player
            if(!cam.checkLock())
            {
                transform.rotation = Quaternion.Euler(0f, cam.getAngle(), 0f);
            }
        }
        else
        {
            isMove = false;
            isRun = false;
        }
        moveDirection = new Vector3(moveH, 0.0f, moveV);       //Create the player's movement from keyboard in local space
        moveDirection = transform.TransformDirection(moveDirection);      //Transform the moveMent from local space to world space
        moveDirection *= speed;      //Based on base speed

        /*****Check current movement mode*****/
        /* Walking, Running
         */
        if (Input.GetButtonDown("Run"))
        {
            isRun = !isRun;
            if(isRun)
                speed *= 3;
            else
                speed = ispeed;     //Set the speed back to initial
        }

        /*****Check jump mode at last*****/
        if (Input.GetButtonDown("Jump"))               //jump if the character is grounded and the user presses the jump button.
        {
            jumpDirection.y = jumpSpeed;     //Give a jump speed to player
        }
        //Check if the player jumped
        anim.SetBool("isGround", isGrounded());


        /*****Move the player*****/
        controller.Move(moveDirection * Time.deltaTime);    //move the character based on the gravitational force.
        if (!isGrounded())
        {
            jumpDirection.y -= gravity * Time.deltaTime;
        }
        controller.Move(jumpDirection * Time.deltaTime);

        /*****Set the animator*****/
        anim.SetFloat("Horizontal", moveH);
        anim.SetFloat("Vertical", moveV);
        anim.SetBool("isMove", isMove);
        anim.SetBool("isRun", isRun);
    }

    private void warpStrike()
    {
        anim.Play("Throwing",0);
    }

    public void off()
    {
        Transform sword = Sword.transform;
        Sword.transform.parent = null;

        anim.enabled = false;

        Sword.transform.up = cam.getTarget().position - transform.position;
        swordMove = true;
        
    }

    public void warp()
    {
        transform.position = cam.getTarget().position - 1.0f * (cam.getTarget().position - transform.position).normalized;
        swordMove = false;

        anim.enabled = true;

        Sword.transform.SetParent(RightHand.transform);
        Sword.transform.localPosition = new Vector3(-0.105f, 0.102f, 0.054f);
        Sword.transform.localRotation = Quaternion.Euler(new Vector3(7.665f, 28.308f, 35.802f));

        Instantiate(particle2, transform.position, Quaternion.identity);

        anim.Play("StrikeAttack", 0);
    }

    public void setAim(bool Aim)
    {
        isAim = Aim;
        anim.SetBool("isAim", isAim);
    }

    public void setMelee(bool Melee)
    {
        isMelee = Melee;
        anim.SetBool("isMelee", isMelee);
    }

    public void setController()
    {
        jumpDirection.y = 0.0f;
        controller.enabled = true;
    }

    //This built-in function will be called after the script first time loaded into the scene
    void Start()
    {
        isMove = false;
        isRun = false;
        swordMove = false;

        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cam = transform.GetChild(1).GetComponentInChildren<ThirdPersonCamera>();

        ispeed = speed;
    }

    void Update()
    {
        if (Input.GetButtonDown("Strike"))
        {
            Instantiate(particle1, transform.position, transform.rotation);
            controller.enabled = false;
            warpStrike();
        }

        checkMove();
        move();

        if (swordMove)
        {
            Sword.transform.position = Vector3.MoveTowards(Sword.transform.position, cam.getTarget().position, 70 * Time.deltaTime);
            if(Sword.transform.position == cam.getTarget().position)
            {
                warp();
            }
        }
    }

}
