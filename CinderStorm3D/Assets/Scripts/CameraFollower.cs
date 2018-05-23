using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public GameObject cameraSubject;
    
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0, cameraSubject.transform.position.y + 10.0f, this.transform.position.z);
    }
}
