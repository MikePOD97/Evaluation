using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMove : MonoBehaviour {

    // Use this for initialization
    //Variables to freely change the robot's position and speed
    private Vector3 pos;
    private Vector3 speed;
    private Animator animator;
    private bool isIdle = true;
    private bool isWalking = false;
	void Start ()
    {
        //Initialize them
        pos = new Vector3(0, 0, 0);
        speed = new Vector3(2f, 0, 0);
        animator = GameObject.Find("PlayerRobot").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed.x);
            if(!isWalking)
            {
                isWalking = true;
                isIdle = false;
               
            }
        }
           
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * (-1 * speed.x));
            if (!isWalking)
            {
                isWalking = true;
                isIdle = false;
               
            }
        }
        else
        {
            isWalking = false;
            isIdle = true;
          
        }
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isWalking", isWalking);
    }
}
