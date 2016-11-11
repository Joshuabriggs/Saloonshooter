using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour {

    public int m_health = 10;
    private int m_path;
    [SerializeField]
    private float m_deviation = 60f;
    [SerializeField]
    private float m_speed = 8f;
    [SerializeField]
    private float m_attacktimer = 30f;
    private float m_traveldistance;
    private float m_distancetraveled;
    private Vector3 m_startLocation;
    private Vector3 m_shotSpawn;
    private int m_travelling;
    private Transform m_transform;
    private Transform m_guntransform;
    private Rigidbody m_body;
    public GameObject m_bullet;


    // Use this for initialization
    void Start()
    {

        m_transform = transform;
        m_path = Random.Range(1, 4);
        m_travelling = 1;
        m_body = GetComponent<Rigidbody>();
        m_traveldistance = Random.Range(15, 50);
        m_startLocation = m_transform.position;
        m_guntransform = GetComponentInChildren<Transform>();


    }

    // Update is called once per frame
    void Update()
    {
        m_distancetraveled = m_transform.position.z - m_startLocation.z;

        m_shotSpawn = m_guntransform.position;

        if(m_distancetraveled >= m_traveldistance)
        {
            m_travelling = 2;
        }

        if (m_travelling == 1)
        {
            Moving();
        }

        if (m_travelling == 2)
        {
            Attacking();
        }

        switch (m_travelling)
        {
            case 1:
                Moving();
                break;

            case 2:
                Attacking();
                break;

            case 3:
                Avoid();
                break;
        }


        if (m_health <= 0)
        {
            Destroy(gameObject);
        }

    }

    void Moving()
    {
        if (m_deviation >= 1)
        {
            if (m_path == 1)
            {
                m_transform.eulerAngles = new Vector3(0, 45);
            }

            if (m_path == 3)
            {
                m_transform.eulerAngles = new Vector3(0, -45);
            }

            m_deviation -= 1;
        }

        if (m_deviation <= 0)
        {
            m_transform.eulerAngles = new Vector3(0, 0, 0);
        }

        m_transform.Translate(new Vector3(0, 0, 1) * m_speed * Time.deltaTime);

    }

    void Attacking()
    {

        if (m_attacktimer <= 0)
        {
            m_body.AddForce(new Vector3(0, 100f, 0));
            m_attacktimer = 30f;
            Instantiate(m_bullet, m_shotSpawn, Quaternion.identity);
        }

        m_transform.eulerAngles = new Vector3(0, 0, 0);

        m_attacktimer -= 1;

        



    }

    void Avoid()
    {

        if (m_deviation >= 1)
        {
            if (m_path == 1)
            {
                m_transform.eulerAngles = new Vector3(0, 45);
            }

            if (m_path == 2)
            {
                m_transform.eulerAngles = new Vector3(0, -45);
            }

            m_deviation -= 1;
        }

        else
        {
            m_travelling = 1;
        }

        m_transform.Translate(new Vector3(0, 0, 1) * m_speed * Time.deltaTime);

    }
    void OnCollisionEnter(Collision m_col)
    {

        if (m_col.gameObject.tag == "Bullet")
        {
            m_health -= 5;
        }


    }

    void OnTriggerEnter(Collider m_trig)
    {
        if (m_trig.gameObject.tag == "Bar")
        {
            m_travelling = 2;
        }

        if (m_trig.gameObject.tag == "Enemy")
        {
            m_travelling = 3;
            m_path = Random.Range(1, 3);
            m_deviation = 60f;
        }


    }
}
