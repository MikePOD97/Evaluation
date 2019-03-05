using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboMove : MonoBehaviour {

    // Use this for initialization
    //Variables to freely change the robot's position and speed
    private Vector3 pos;
    private Vector3 speed;
    private Vector3 rot;
    private bool jumped = false;
    public bool onGround = false;
    public int jumpSpeed = 3;
    public Animator animator;
    private bool isIdle = true;
    private bool isWalking = false;
	void Start ()
    {
        //Initialize them
        pos = new Vector3(0, 0, 0);
        speed = new Vector3(2f, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ((gameObject.tag == "Player 1" && Input.GetKey(KeyCode.D)) || (gameObject.tag == "Player 2" && Input.GetKey(KeyCode.RightArrow)))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed.x);
            if (transform.rotation.y != 90) rot = new Vector3(0, 90, 0);
            if (!isWalking)
            {
                isWalking = true;
                isIdle = false;
            }
        }
           
        else if ((gameObject.tag == "Player 1" && Input.GetKey(KeyCode.A)) || (gameObject.tag == "Player 2" && Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.Translate((-1 * Vector3.forward) * Time.deltaTime * (-1 * speed.x));
            if (transform.rotation.y != -90) rot = new Vector3(0, -90, 0);
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

        if (transform.position.y < -3) onGround = true;
        if ((gameObject.tag == "Player 1" && Input.GetKeyDown(KeyCode.W) && onGround == true) || (gameObject.tag == "Player 2" && Input.GetKeyDown(KeyCode.UpArrow) && onGround == true)) Jump();
        if ((gameObject.tag == "Player 1" && Input.GetKeyDown(KeyCode.S)) || (gameObject.tag == "Player 2" && Input.GetKeyDown(KeyCode.DownArrow))) Punch();
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isWalking", isWalking);
        transform.rotation = Quaternion.Euler(rot);
    }

    private void Jump()
    {
        GetComponent<Rigidbody>().velocity = Vector2.up * jumpSpeed;
        onGround = false;
        animator.SetTrigger("Jump");
    }

    private void Punch()
    {
        animator.SetTrigger("Punch");
        animator.ResetTrigger("Jump");
    }
}
