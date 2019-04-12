using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0F;          //the moving speed of the character

    public float rotateSpeed = 3.0F;    //the rotate speed of the character

    public float jumpSpeed = 18.0f;      //the jump force of the character

    public float gravity = 20.0f;       //the force of gravity on the character

    public float GroundOffset = .2f;    //the offset for the IsGrounded check. Useful for recognizing slopes and imperfect ground.

    private Vector3 moveDirection = Vector3.zero;   //the direction the character should move.

    private CharacterController controller;

    private GameObject head;      //The main camera of player, used to rotate the camera

    private Rigidbody rb;       //The rigidbody used for physics

    private float rotationX, rotationY;        //The rotation parameter


    //This built-in function will be called after the script first time loaded into the scene
    void Awake()
    {
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();

        head = this.gameObject.transform.GetChild(0).gameObject;

        rb = GetComponent<Rigidbody>();     //Get the rigidbody
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

    void Update()
    {
        // Rotate the object
        rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotateSpeed;
        rotationY -= Input.GetAxis("Mouse Y") * rotateSpeed;
        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        head.transform.localEulerAngles = new Vector3(rotationY, 0, 0);


        // Move the character     
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));       //Create the player's movement from keyboard in local space
        moveDirection = transform.TransformDirection(moveDirection);      //Transform the moveMent from local space to world space
        moveDirection *= speed;      //Based on base speed

        if (Input.GetButtonDown ("Jump"))               //jump if the character is grounded and the user presses the jump button.
        {
            rb.AddForce(jumpSpeed * Vector3.up, ForceMode.Impulse);     //Give a force to player
        }

        controller.Move(moveDirection * Time.deltaTime);    //move the character based on the gravitational force.
    }

}
