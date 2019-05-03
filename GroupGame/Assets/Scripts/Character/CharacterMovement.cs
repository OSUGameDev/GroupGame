using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0F;          //the moving speed of the character
    public float jumpSpeed = 10.0f;      //the jump force of the character

    public float gravity = 20.0f;       //the force of gravity on the character

    public float GroundOffset = .2f;    //the offset for the IsGrounded check. Useful for recognizing slopes and imperfect ground.

    private float moveH, moveV;
    private Vector3 moveDirection = Vector3.zero;   //the direction the character should move.
    private Vector3 jumpDirection = Vector3.zero;
    private bool isMove, isRun;

    private CharacterController controller;
    private Animator anim;

    private ThirdPersonCamera cam;


    //This built-in function will be called after the script first time loaded into the scene
    void Start()
    {
        this.isMove = false;
        this.isRun = false;

        Cursor.visible = false;

        this.controller = GetComponent<CharacterController>();
        this.anim = GetComponent<Animator>();
        this.cam = this.transform.GetChild(2).GetComponentInChildren<ThirdPersonCamera>();
    }

    ///The check to see if the character is currently on the ground.
    private bool isGrounded(){
        RaycastHit hit;
        Physics.Raycast(this.transform.position, -this.transform.up,out hit, 10);   //A short ray shot directly downward from the center of the character.

        if(System.Math.Abs(hit.distance) < System.Single.Epsilon)                                           //if the distance is zero, the ray probably did not hit anything.
        {
            return false;
        }
        if(hit.distance <= (this.transform.lossyScale.y/2 +GroundOffset))   //if the distance from the ray is less than half the height 
        {                                                                   //of the character (plus the offset), the character us grounded.
            return true;
        }
        return false;
    }

    private void move()
    {
        // Move the character   
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        if (moveH != 0 || moveV != 0)
        {
            isMove = true;
            this.transform.rotation = Quaternion.Euler(0f, cam.getAngle(), 0f);
        }
        else
        {
            isMove = false;
            isRun = false;
        }

        moveDirection = new Vector3(moveH, 0.0f, moveV);       //Create the player's movement from keyboard in local space
        moveDirection = transform.TransformDirection(moveDirection);      //Transform the moveMent from local space to world space
        moveDirection *= speed;      //Based on base speed
        if (Input.GetButtonDown("Run"))
        {
            this.isRun = !this.isRun;
            moveDirection *= speed;
        }

        anim.SetFloat("Horizontal", moveH);
        anim.SetFloat("Vertical", moveV);
        anim.SetBool("isMove", isMove);
        anim.SetBool("isRun", isRun);

        if (Input.GetButtonDown("Jump"))               //jump if the character is grounded and the user presses the jump button.
        {
            jumpDirection.y = jumpSpeed;     //Give a jump speed to player
        }

        controller.Move(moveDirection * Time.deltaTime);    //move the character based on the gravitational force.
        jumpDirection.y -= gravity * Time.deltaTime;
        controller.Move(jumpDirection * Time.deltaTime);
    }

    void Update()
    {
        move();
    }

}
