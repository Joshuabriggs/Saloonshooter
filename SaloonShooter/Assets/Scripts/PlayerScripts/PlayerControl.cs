using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float MovementSpeed=5.0f; //How fast does the player move
    public float HideHeight = 20.0f; //How low does the player hide

    public float PlayerHealth; //Players health (for later use)

    private Rigidbody rbody;

    private float currentSpeed; //Players current speed which is affected by crouching
    private bool isCrouched=false; //Crouch flag


	// Use this for initialization
	void Start () {
        rbody = this.GetComponent<Rigidbody>(); //Get the player rigidbody
	}
	
	// Update is called once per frame
	void Update () {
        SpeedControl();
    }

    void FixedUpdate() {
        Movement();
    }

    //Player movement
    private void Movement() {
        //Move Left
        if (Input.GetKey(KeyCode.A))
        {
            rbody.MovePosition(transform.position + transform.forward * currentSpeed * Time.deltaTime);
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            rbody.MovePosition(transform.position + transform.forward * -currentSpeed * Time.deltaTime);

        }
        //Crouch
        if (Input.GetKeyDown(KeyCode.S) && isCrouched == false)
        {
            
            crouch();
            rbody.MovePosition(transform.position + transform.up * -HideHeight * Time.deltaTime);
        }
        //Stand Up
        if (Input.GetKeyDown(KeyCode.W) && isCrouched == true)
        {           
            stopCrouching();
            rbody.MovePosition(transform.position + transform.up * HideHeight * Time.deltaTime);
        }
    }

    private void crouch() {
        this.GetComponent<BoxCollider>().size -= new Vector3(0, .8f, 0);
        this.GetComponent<BoxCollider>().center -= new Vector3(0, -.2f, 0);
        isCrouched = true;
    }

    private void stopCrouching() {
        this.GetComponent<BoxCollider>().size += new Vector3(0, .8f, 0);
        this.GetComponent<BoxCollider>().center += new Vector3(0, -.2f, 0);
        isCrouched = false;
    }

    //Depending the state the player is in adjust the speed accordingly
    private void SpeedControl() {
        if (isCrouched)
        {   //If the player is crouching half the speed they can move at
            currentSpeed = MovementSpeed / 2f;
        }
        else
        { //If the player is standing up use default speed
            currentSpeed = MovementSpeed;
        }
    }
}
