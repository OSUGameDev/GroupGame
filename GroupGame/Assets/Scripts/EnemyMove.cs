using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3.0F;          //the moving speed of the character
    public float rotateSpeed = 3.0F;    //the rotate speed of the character
    public float jumpSpeed = 8.0f;      //the jump force of the character
    public float gravity = 20.0f;       //the force of gravity on the character
    public float GroundOffset = .2f;    //the offset for the IsGrounded check. Useful for recognizing slopes and imperfect ground.
    private Vector3 moveDirection = Vector3.zero;   //the direction the character should move.
    public int movementMode;  //maybe have a confusion mode that gives a special animation
//
    void Start()
    {
        movementMode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
        void move(){ // movement ai
        CharacterController controller = GetComponent<CharacterController>();
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        switch(movementMode){
            case 0: //looking for enemy
                controller.SimpleMove(transform.TransformDirection(forward)*speed);
                break;
            case 1: //engaging enemy
                //stop
                //if enemy is x distance away then chase
                break;
            case 2: //dead
                break;
            }

    }
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
}

