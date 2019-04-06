using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMove : MonoBehaviour
{
    public float speed = 3.0F;
    public float rotateSpeed = 3.0F;

    public float jumpSpeed = 8.0f;

    public float gravity = 20.0f;
    public Vector3 moveDirection = Vector3.zero;
    
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

        if (Input.GetButtonDown ("Jump"))
        {
            if(controller.isGrounded)
            { 
                moveDirection.y= jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
