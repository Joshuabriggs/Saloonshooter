using UnityEngine;
using System.Collections;

public class EnemyMain : MonoBehaviour
{
    public float m_health;
    private int m_path;
    [SerializeField]
    private float m_deviation = 60f;
    [SerializeField]
    private float m_speed = 5f;
    [SerializeField]
    private float m_attacktimer = 30f;
    private float m_traveldistance;
    private float m_distancetraveled;
    private Vector3 m_startLocation;
    [HideInInspector]
    public int m_travelling;
    private int m_enemytype;
    private Transform m_transform;
    private Rigidbody m_body;
    private GameObject m_player;
    private Vector3 m_relativePos;
    private Quaternion m_rotation;

    // Use this for initialization
    void Start()
    {

        m_transform = transform;
        m_path = Random.Range(1, 4);
        m_travelling = 1;
        m_body = GetComponent<Rigidbody>();
        m_player = GameObject.Find("PlayerBody");

        if (gameObject.name == "Enemy1" || gameObject.name == "Enemy1(Clone)")
        {
            m_enemytype = 1;
            m_health = 10f;
            m_traveldistance = 40f;
            m_startLocation = m_transform.position;

        }
        if (gameObject.name == "Enemy2" || gameObject.name == "Enemy2(Clone)")
        {
            m_enemytype = 2;
            m_traveldistance = Random.Range(10, 30);
            m_startLocation = m_transform.position;
            m_health = 10f;
        }

    }

    // Update is called once per frame
    void Update()
    {



        switch (m_travelling)
        {
            case 1:
                switch (m_enemytype)
                {
                    case 1:
                        Moving();
                        break;
                    case 2:
                        Moving2();
                        break;

                }
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

        m_distancetraveled = m_transform.position.z - m_startLocation.z;

        if (m_distancetraveled >= m_traveldistance)
        {
            m_relativePos = m_player.transform.position - m_transform.position;
            m_rotation = Quaternion.LookRotation(m_relativePos);
            m_transform.rotation = m_rotation;
            m_speed = 20f;
        }

        else
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

        m_transform.Translate(new Vector3(0, 0, 1) * m_speed * Time.deltaTime);

    }

    void Moving2()
    {

        m_distancetraveled = m_transform.position.z - m_startLocation.z;

        if (m_distancetraveled >= m_traveldistance)
        {
            m_travelling = 2;
        }
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
        m_relativePos = m_player.transform.position - m_transform.position;
        m_rotation = Quaternion.LookRotation(m_relativePos);
        m_transform.rotation = m_rotation;

       
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

    void OnCollisionEnter(Collision m_col)
    {
        if (m_col.gameObject.name == "Bottle")
        {
            m_health -= 10;
        }
    }
}


