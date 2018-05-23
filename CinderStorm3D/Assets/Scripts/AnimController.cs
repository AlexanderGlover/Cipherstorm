using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {
    public Animator animController;

    private bool facingRight = false;
    private bool isJumping = true;

    public void UpdateAnim(Vector3 movementVector) {
        if(isJumping)
        {
            return;
        }
        if (Mathf.Abs(movementVector[0]) > 0)
        {
            animController.SetBool("hasHorizontalVelocity", true);
            animController.SetFloat("horizontalVelocity", Mathf.Max(Mathf.Sqrt(Mathf.Abs(movementVector[0])), 0.1f) * 3);

            if(movementVector[0] < 0 && !facingRight)
            {
                facingRight = !facingRight;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            else if(movementVector[0] > 0 && facingRight)
            {
                facingRight = !facingRight;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            animController.SetBool("hasHorizontalVelocity", false);
        }
    }

    public void JumpAnimOverride(bool inputIsJumping)
    {
        animController.SetBool("isJumping", inputIsJumping);
        isJumping = inputIsJumping;
    }
    
    public void WallHitAnimOverride(bool onWall)
    {
        animController.SetBool("onWall", onWall);
    }
}
