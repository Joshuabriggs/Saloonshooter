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
    private float m_xDrunkmultiply = 0.5f;
    private float m_zDrunkmultiply = 0.5f;
    private int m_zDelay = 10;
    private int m_xDrunkcount = 25;
    private int m_zDrunkcount = 25;
    private bool isCrouched = false; //Crouch flag


    private bool keyLeft, keyRight, KeyCrouch;

    [SerializeField] GameObject m_clickTut;
    [SerializeField] GameObject m_moveTut;

    // Update is called once per frame
    void Update()
    {
        MovementSpeed = GameState.instance.m_playerSpeed;
        SpeedControl();
        GetInput();
        Drunk();
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            keyLeft = true;
            m_moveTut.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            keyLeft = false;
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            keyRight = true;
            m_moveTut.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            keyRight = false;
        }
        //Crouch
        if (Input.GetKey(KeyCode.LeftControl))
        {
            KeyCrouch = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            KeyCrouch = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameState.instance.m_currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameState.instance.m_currentWeapon = 2;
        }
        if (Input.GetMouseButtonDown(0))
        {
            m_clickTut.SetActive(false);
        }
    }

    private void Movement()
    {
        //Move Left
        if (keyLeft)
        {
            transform.Translate(transform.right * currentSpeed * Time.deltaTime);
        }
        //Move Right
        if (keyRight)
        {
            transform.Translate(transform.right * -currentSpeed * Time.deltaTime);
        }
        //Crouch
        if (KeyCrouch && !isCrouched)
        {
            crouch();
            transform.Translate(transform.up * -HideHeight * Time.deltaTime);
        }
        //Stand Up
        if (!KeyCrouch && isCrouched)
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
                transform.Rotate(new Vector3(0.2f, 0, 0) * m_xDrunkmultiply, Space.World);

                if(m_zDelay < 0)
                {
                    transform.Rotate(new Vector3(0, 0, -0.2f) * m_zDrunkmultiply, Space.World);
                    m_zDrunkcount++;
                }

                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);
                m_xDrunkcount++;
                if (m_xDrunkcount > 50)
                {
                    m_xDrunkmultiply *= -1;
                    m_xDrunkcount = 0;
                }
                if (m_zDrunkcount > 50)
                {
                    m_zDrunkmultiply *= -1;
                    m_zDrunkcount = 0;
                }
                m_zDelay--;

                break;

            case 2:
                transform.Rotate(new Vector3(0.5f, 0, 0) * m_xDrunkmultiply, Space.World);
                transform.Rotate(new Vector3(0, 0, -0.5f) * m_zDrunkmultiply, Space.World);

                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);

                m_xDrunkcount++;
                m_zDrunkcount++;

                if (m_xDrunkcount > 40)
                {
                    m_xDrunkmultiply *= -1;
                    m_xDrunkcount = 0;
                }
                if (m_zDrunkcount > 40)
                {
                    m_zDrunkmultiply *= -1;
                    m_zDrunkcount = 0;
                }

                break;

            case 3:
                transform.Rotate(new Vector3(1, 0, 0) * m_xDrunkmultiply, Space.World);
                transform.Rotate(new Vector3(0, 0, -1) * m_zDrunkmultiply, Space.World);


                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);


                m_xDrunkcount++;
                m_zDrunkcount++;
                if (m_xDrunkcount > 30)
                {
                    m_xDrunkmultiply *= -1;
                    m_xDrunkcount = 0;
                }
                if (m_zDrunkcount > 30)
                {
                    m_zDrunkmultiply *= -1;
                    m_zDrunkcount = 0;
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

