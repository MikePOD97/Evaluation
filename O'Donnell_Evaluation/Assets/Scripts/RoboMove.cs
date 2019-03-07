using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoboMove : MonoBehaviour {

    // Use this for initialization
    //Variables to keep track of robot's speed and rotation
    private Vector3 speed;
    private Vector3 rot;
    //Robot's health
    public int health = 100;
    //Variables to manage punching
    private bool punched;
    public float punchTime;
    //Variables to manage jumping
    public bool onGround = false;
    public int jumpSpeed = 3;
    //Variables to manage the animator
    public Animator animator;
    private bool isIdle = true;
    private bool isWalking = false;
	void Start ()
    {
        //Initialize speed
        speed = new Vector3(2f, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Based on the controls of the player, move them right...
        if ((gameObject.tag == "Player 1" && Input.GetKey(KeyCode.D)) || (gameObject.tag == "Player 2" && Input.GetKey(KeyCode.RightArrow)))
        {
            //...using their set speed value and their forward
            transform.Translate(Vector3.forward * Time.deltaTime * speed.x);
            //Flip them if need be
            if (transform.rotation.y != 90) rot = new Vector3(0, 90, 0);
            //Manage the current animation so they don't cut each other off (too much)
            if (!isWalking)
            {
                isWalking = true;
                isIdle = false;
            }
        }
        //Same but for left
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
        //If they aren't moving, make them idle
        else
        {
            isWalking = false;
            isIdle = true;
        }
        //If they are touching the ground, then can jump
        if (transform.position.y < -3) onGround = true;
        if ((gameObject.tag == "Player 1" && Input.GetKeyDown(KeyCode.W) && onGround == true) || (gameObject.tag == "Player 2" && Input.GetKeyDown(KeyCode.UpArrow) && onGround == true)) Jump();
        //Controls for punching
        if ((gameObject.tag == "Player 1" && Input.GetKeyDown(KeyCode.S)) || (gameObject.tag == "Player 2" && Input.GetKeyDown(KeyCode.DownArrow))) Punch();
        //Manage animations
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isWalking", isWalking);
        //Set rotation accordingly
        transform.rotation = Quaternion.Euler(rot);
        //If a player runs out of health, go to the end screen with the appropriate player number saved
        if(health == 0)
        {
            PlayerPrefs.SetString("Loser", gameObject.tag);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //Handles Jumping
    private void Jump()
    {
        //Moves them up by the set speed, tells the game they aren't on the ground, start the animation
        GetComponent<Rigidbody>().velocity = Vector2.up * jumpSpeed;
        onGround = false;
        animator.SetTrigger("Jump");
    }
    //Handles Punching
    private void Punch()
    {
        //Set the animation, start the countdown for the punch, and reset the jump trigger
        animator.SetTrigger("Punch");
        StartCoroutine(StartPunchTimer());
        animator.ResetTrigger("Jump");
    }
    //Handles getting hit by a punch
    private void OnTriggerEnter(Collider other)
    {
        //If the collider isn't the fist, there's no need to continue
        if (other.gameObject.layer != 8 && other.gameObject.layer != 9) return;
        if(punched)
        {
            //Make sure to adjust the appropriate health level
            if(gameObject.tag == "Player 1")
            {
                GameObject.FindGameObjectWithTag("Player 2").GetComponent<RoboMove>().health -= 10;
            }
            else if(gameObject.tag == "Player 2")
            {
                GameObject.FindGameObjectWithTag("Player 1").GetComponent<RoboMove>().health -= 10;
            }
            //Make it so they aren't continuously punched
            punched = false;
        }
        
    }
    //This handles the punch so that the hitbox is only active on the correct frame
    public IEnumerator StartPunchTimer()
    {
        //Set the punch time
        punchTime = 2.0f;
        //Count down
        while (punchTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            punchTime--;
            //Set the punch to be active on the right frame
            if (punchTime == 1.0f) punched = true;
        }
        //Again, make sure there are no multiple punchies
        punched = false;
    }
}
