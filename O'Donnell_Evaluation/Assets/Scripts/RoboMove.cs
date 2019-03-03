using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMove : MonoBehaviour {

    // Use this for initialization
    Vector3 pos;
    Vector3 speed;
	void Start ()
    {
        pos = new Vector3(0, 0, 0);
        speed = new Vector3(0.1f, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.D))
            pos.x += speed.x;
        if (Input.GetKey(KeyCode.A))
            pos.x -= speed.x;
        transform.position = pos;
	}
}
