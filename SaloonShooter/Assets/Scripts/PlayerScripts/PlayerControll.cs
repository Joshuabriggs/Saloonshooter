using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;


public class PlayerControll : MonoBehaviour
{

    public float MovementSpeed = 8.0f; //How fast does the player move
    public float HideHeight = 50.0f; //How low does the player hide

    public float PlayerHealth; //Players health (for later use)

    private float currentSpeed; //Players current speed which is affected by crouching
    [HideInInspector]
    public int m_drunkspin = 0;
    private float m_drunkmultiply = 0.5f;
    private int m_drunkcount = 5;
    private bool isCrouched = false; //Crouch flag


    private bool keyLeft, keyRight, KeyCrouch;

<<<<<<< HEAD

    // Update is called once per frame
    void Update()
    {
=======
	// Update is called once per frame
	
    // Update is called once per frame
    void Update()
    {

>>>>>>> origin/master
        SpeedControl();
        GetInput();
        Drunk();
    }

    void FixedUpdate()
    {
        Movement(); 
    }

    private void GetInput() {    
        if (Input.GetKey(KeyCode.A))
        {
            keyLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            keyLeft = false;
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            keyRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            keyRight = false;
        }
        //Crouch
        if (Input.GetKey(KeyCode.C))
        {
            KeyCrouch = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            KeyCrouch = false;
        }
    }

    //Player movement
<<<<<<< HEAD
    private void Movement()
    {
        //Move Left
        if (Input.GetKey(KeyCode.A))
            if (keyLeft)
            {
                transform.Translate(transform.right * currentSpeed * Time.deltaTime);
            }
=======
    private void Movement() {
        if (keyLeft)
        {
            transform.Translate(transform.right * currentSpeed * Time.deltaTime);
        }
>>>>>>> origin/master
        //Move Right
        if (Input.GetKey(KeyCode.D))
            if (keyRight)
            {
                transform.Translate(transform.right * -currentSpeed * Time.deltaTime);

            }
        //Crouch
<<<<<<< HEAD
            if (KeyCrouch && !isCrouched)
            {
=======
        if (KeyCrouch && !isCrouched)
        {
            crouch();
            transform.Translate(transform.up * -HideHeight * Time.deltaTime);
        }
        if(!KeyCrouch && isCrouched){
>>>>>>> origin/master

                crouch();
                transform.Translate(transform.up * -HideHeight * Time.deltaTime);
            }
        //Stand Up
<<<<<<< HEAD
            if (!KeyCrouch && isCrouched)
            {
                stopCrouching();
                transform.Translate(transform.up * HideHeight * Time.deltaTime);
            }
=======
        if (Input.GetKeyDown(KeyCode.W) && isCrouched == true)
        {

            stopCrouching();
            transform.Translate(transform.up * HideHeight * Time.deltaTime);
        }
>>>>>>> origin/master
    }

    private void Drunk()
    {       
        switch (m_drunkspin)
        {
            case 0:
                break;

            case 1:
                transform.Rotate(new Vector3(1f, 0, -1) * m_drunkmultiply, Space.World);

                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);
                m_drunkcount++;
                if (m_drunkcount > 10)
                {
                    m_drunkmultiply *= -1;
                    m_drunkcount = 0;
                }

                break;

            case 2:
                transform.Rotate(new Vector3(1, 0, -1) * m_drunkmultiply, Space.World);

                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);

                m_drunkcount++;
                if (m_drunkcount > 20)
                {
                    m_drunkmultiply *= -1;
                    m_drunkcount = 0;
                }

                break;

            case 3:
                transform.Rotate(new Vector3(1, 0, -1) * m_drunkmultiply, Space.World);


                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);


                m_drunkcount++;
                if (m_drunkcount > 30)
                {
                    m_drunkmultiply *= -1;
                    m_drunkcount = 0;
                }

                break;

        }

        if (m_drunkspin > 0)
        {
            GetComponentInChildren<BlurOptimized>().enabled = true;
            GetComponentInChildren<BlurOptimized>().blurSize = m_drunkspin - 1;

        }
        if (m_drunkspin <= 0)
        {
            GetComponentInChildren<BlurOptimized>().enabled = false;
        }


    }

    private void crouch()
    {
        this.GetComponent<BoxCollider>().size -= new Vector3(0, .8f, 0);
        this.GetComponent<BoxCollider>().center -= new Vector3(0, -.2f, 0);
        isCrouched = true;
    }

    private void stopCrouching()
    {
        this.GetComponent<BoxCollider>().size += new Vector3(0, .8f, 0);
        this.GetComponent<BoxCollider>().center += new Vector3(0, -.2f, 0);
        isCrouched = false;
    }

    //Depending the state the player is in adjust the speed accordingly
    private void SpeedControl()
    {
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

