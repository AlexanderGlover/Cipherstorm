using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float horizontalRampSpeed;
    public float movementDecaySpeed;
    public float jumpForce; //Initial Verticle Velocity
    public float inAirJumpModifier;
    public float appliedVelocityMultiplier;
    public float maxHorizontalSpeed;

    private Vector3 movementVector = new Vector3(0, 0, 0); //Stored as {Horizontal, Vertical, Dummy = 0}
    private bool mCanJump = false;
    private bool mOnWall = false;
    private Rigidbody rigidBody;
    private AnimController animController;

    private int layerMask;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>() as Rigidbody;
        animController = this.gameObject.GetComponent(typeof(AnimController)) as AnimController;

        //Hit everything under layer 8
        layerMask = 1 << 8;
        layerMask = ~layerMask;
    }

    void FixedUpdate()
    {
        // Get the velocity
        Vector3 horizontalMove = rigidBody.velocity;
        // Don't use the vertical velocity
        horizontalMove.y = 0;
        // Calculate the approximate distance that will be traversed
        float distance = horizontalMove.magnitude * Time.fixedDeltaTime;
        // Normalize horizontalMove since it should be used to indicate direction
        horizontalMove.Normalize();
        RaycastHit hit;

        // Check if the body's current velocity will result in a collision
        if (rigidBody.SweepTest(horizontalMove, out hit, distance))
        {
            // If so, stop the movement
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
        }
        else
        {
            //Rigid Body deals with y and frozen in z, movement vector should only have x component
            rigidBody.velocity = new Vector3(Mathf.Clamp(rigidBody.velocity.x + (movementVector[0]) * appliedVelocityMultiplier, -maxHorizontalSpeed, maxHorizontalSpeed), rigidBody.velocity.y, 0.0f);
            animController.UpdateAnim(movementVector);
        }
        MovementDecay();

        if(rigidBody.velocity.y == 0.0f)
        {
            mCanJump = true;
            animController.JumpAnimOverride(false);
        }

        if(mOnWall && !((Physics.Raycast(transform.position + new Vector3(0.0f, 0.15f, 0.0f), transform.TransformDirection(Vector3.left), 5.0f, layerMask) && Physics.Raycast(transform.position + new Vector3(0.0f, -0.15f, 0.0f), transform.TransformDirection(Vector3.left), 5.0f, layerMask)) || (Physics.Raycast(transform.position + new Vector3(0.0f, 1.5f, 0.0f), transform.TransformDirection(Vector3.right), 5.0f, layerMask) && Physics.Raycast(transform.position + new Vector3(0.0f, -1.5f, 0.0f), transform.TransformDirection(Vector3.right), 5.0f, layerMask))))
        {
            mOnWall = false;
            animController.WallHitAnimOverride(false);

        }
    }

    void MovementDecay()
    {
        //How do we stop the player from continuing to move
        movementVector[0] = movementVector[0] > 0.1f ? 0.0f : movementVector[0] * movementDecaySpeed;
        return;
    }

    //--------------------MOVE FUNCTIONS------------------------------
    public void MovingLeft()
    {
        float controlAmount = mCanJump ? 1.0f : inAirJumpModifier;
        movementVector[0] = movementVector[0] - horizontalRampSpeed < -1.0f ? -1.0f : (movementVector[0] - horizontalRampSpeed) * controlAmount;
    }

    public void MovingRight()
    {
        float controlAmount = mCanJump ? 1.0f : inAirJumpModifier;
        movementVector[0] = movementVector[0] + horizontalRampSpeed > 1.0f ? 1.0f : (movementVector[0] + horizontalRampSpeed) * controlAmount;
    }


    public void MovingUp()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>() as Rigidbody;
        if(mCanJump)
        {
            mCanJump = false;
            rigidBody.AddForce(0,jumpForce,0);
            animController.JumpAnimOverride(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
            








        //JumpEnding
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 2.0f, layerMask))
        {
            mCanJump = true;
            animController.JumpAnimOverride(false);
            return;
        }
        
        //Wall Slide Impact
        if ((Physics.Raycast(transform.position + new Vector3(0.0f, 0.15f, 0.0f), transform.TransformDirection(Vector3.left), 5.0f, layerMask) && Physics.Raycast(transform.position + new Vector3(0.0f, -0.15f, 0.0f), transform.TransformDirection(Vector3.left), 5.0f, layerMask)) || (Physics.Raycast(transform.position + new Vector3(0.0f, 1.5f, 0.0f), transform.TransformDirection(Vector3.right), 5.0f, layerMask) && Physics.Raycast(transform.position + new Vector3(0.0f, -1.5f, 0.0f), transform.TransformDirection(Vector3.right), 5.0f, layerMask)))
        {
            mCanJump = true;
            mOnWall = true;
            animController.WallHitAnimOverride(true);
        }

    }
}
