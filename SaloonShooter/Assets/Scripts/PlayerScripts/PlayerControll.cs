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

    // Update is called once per frame
    void Update()
    {
        SpeedControl();
    }

    void FixedUpdate()
    {
        Movement();
        Drunk();

    }

    void Start()
    {
        

    }

    //Player movement
    private void Movement()
    {
        //Move Left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(transform.right * currentSpeed * Time.deltaTime);
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * -currentSpeed * Time.deltaTime);

        }
        //Crouch
        if (Input.GetKeyDown(KeyCode.S) && isCrouched == false)
        {

            crouch();
            transform.Translate(transform.up * -HideHeight * Time.deltaTime);
        }
        //Stand Up
        if (Input.GetKeyDown(KeyCode.W) && isCrouched == true)
        {
            stopCrouching();
            transform.Translate(transform.up * HideHeight * Time.deltaTime);
        }
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

