using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3.0F;          //the moving speed of the character

    public float rotateSpeed = 3.0F;    //the rotate speed of the character

    public float jumpSpeed = 8.0f;      //the jump force of the character

    public float gravity = 20.0f;       //the force of gravity on the character

    public float GroundOffset = .2f;    //the offset for the IsGrounded check. Useful for recognizing slopes and imperfect ground.

    private Vector3 moveDirection = Vector3.zero;   //the direction the character should move.


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
        CharacterController controller = GetComponent<CharacterController>();

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

        if (Input.GetButtonDown ("Jump") && isGrounded())               //jump if the character is grounded and the user presses the jump button.
        {
            moveDirection.y= jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;        //apply gravity to the character.
        controller.Move(moveDirection * Time.deltaTime);    //move the character based on the gravitational force.
    }

}
