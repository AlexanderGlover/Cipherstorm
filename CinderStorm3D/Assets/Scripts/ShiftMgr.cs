using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftMgr : MonoBehaviour {

    private Vector3 desiredPosition;

    private void Start()
    {
        desiredPosition = transform.position;
    }

    public void MoveBack()
    {
        desiredPosition = transform.position + new Vector3(0.0f,0.0f,5.0f);
    }

    public void MoveForward()
    {
        desiredPosition = transform.position + new Vector3(0.0f, 0.0f, -5.0f);
    }

    public void MoveUp()
    {
        desiredPosition = transform.position + new Vector3(0.0f, 5.0f, 0.0f);
    }

    public void MoveDown()
    {
        desiredPosition = transform.position + new Vector3(0.0f, -5.0f, 0.0f);
    }

    private void Update()
    {
        Vector3 desiredMovement = desiredPosition - transform.position;
        if(desiredMovement.x != 0.0f || desiredMovement.y != 0.0f)
        {
            Debug.Log(desiredMovement);
            Debug.Log(desiredPosition);

        }
        transform.position = transform.position + new Vector3(Mathf.Clamp(desiredMovement.x, -0.01f, 0.01f), Mathf.Clamp(desiredMovement.y, -0.01f, 0.01f), Mathf.Clamp(desiredMovement.z, -0.01f, 0.01f));
    }
}
