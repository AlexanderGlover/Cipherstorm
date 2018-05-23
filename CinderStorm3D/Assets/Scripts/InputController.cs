using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private MovementController movementController;

	// Use this for initialization
	void Start ()
    {
        movementController = this.gameObject.GetComponent(typeof(MovementController)) as MovementController;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(movementController)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movementController.MovingLeft();
            }
            if (Input.GetKey(KeyCode.S))
            {

            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                movementController.MovingUp();
            }
            if (Input.GetKey(KeyCode.D))
            {
                movementController.MovingRight();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {

            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {

            }
        }
    }
}
