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
    private int m_bossmove;
    private Vector3 m_startLocation;
    private Vector3 m_startRotation;
    [HideInInspector]
    public int m_travelling;
    private int m_enemytype;
    [HideInInspector]
    public bool m_atbar;
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
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_startRotation = new Vector3(0, 180, 0);
        m_bossmove = 1;
        m_atbar = false;

        if (gameObject.tag == "Enemy_01")
        {
            m_enemytype = 1;
            m_health = 10f;
            m_traveldistance = 40f;
            m_startLocation = m_transform.position;

        }
        if (gameObject.tag == "Enemy_02")
        {
            m_enemytype = 2;
            m_traveldistance = Random.Range(15, 30);
            m_startLocation = m_transform.position;
            m_health = 10f;
        }
        if (gameObject.name == "EnemyBoss" || gameObject.name == "EnemyBoss(Clone)")
        {
            m_enemytype = 3;
            m_health = 100f;
            m_speed = 2f;
            m_startLocation = m_transform.position;
            m_attacktimer = Random.Range(100, 500);
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
                    case 3:
                        Moving3();
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
            GameState.instance.DestroyEnemy(this);
            Debug.Log("Enemy is dead :(");
        }

    }

    void Moving()
    {

        m_distancetraveled = m_startLocation.z - m_transform.position.z;

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
                    m_transform.eulerAngles = m_startRotation + new Vector3(0, -45);
                }

                if (m_path == 3)
                {
                    m_transform.eulerAngles = m_startRotation + new Vector3(0, 45);
                }

                m_deviation -= 1;
            }

            if (m_deviation <= 0)
            {
                m_transform.eulerAngles = m_startRotation;
            }

            m_transform.Translate(new Vector3(0, 0, 1) * m_speed * Time.deltaTime, Space.Self);
        }

        m_transform.Translate(new Vector3(0, 0, 1) * m_speed * Time.deltaTime);

    }

    void Moving2()
    {

        m_distancetraveled = m_startLocation.z - m_transform.position.z;

        if (m_distancetraveled >= m_traveldistance)
        {
            m_travelling = 2;
        }
        if (m_deviation >= 1)
        {
            if (m_path == 1)
            {
                m_transform.eulerAngles = m_startRotation + new Vector3(0, -45);
            }

            if (m_path == 3)
            {
                m_transform.eulerAngles = m_startRotation + new Vector3(0, 45);
            }

            m_deviation -= 1;
        }

        if (m_deviation <= 0)
        {
            m_transform.eulerAngles = m_startRotation;
        }

        m_transform.Translate(new Vector3(0, 0, 1) * m_speed * Time.deltaTime, Space.Self);

    }

    void Moving3()
    {

        m_distancetraveled = m_startLocation.z - m_transform.position.z;

        if (m_deviation >= 1)
        {
            if (m_path == 1)
            {
                m_transform.eulerAngles = m_startRotation + new Vector3(0, -45);
            }

            if (m_path == 3)
            {
                m_transform.eulerAngles = m_startRotation + new Vector3(0, 45);
            }

            m_deviation -= 1;
        }

        if (m_deviation <= 0)
        {
            m_transform.eulerAngles = m_startRotation;
        }

        m_transform.Translate(new Vector3(0, 0, 1) * m_speed * Time.deltaTime, Space.Self);

        m_attacktimer--;

        if (m_attacktimer <= 0)
        {
            m_travelling = 2;
            m_attacktimer = Random.Range(100, 500);
            if(m_bossmove == 2)
            {
                m_bossmove = -1;
            }
            if(m_bossmove == 0)
            {
                m_bossmove = 1;
            }
        }

        

    }

    void Attacking()
    {
        m_relativePos = m_player.transform.position - m_transform.position;
        m_rotation = Quaternion.LookRotation(m_relativePos);
        m_transform.rotation = m_rotation;

        if(m_enemytype == 3)
        {
            m_attacktimer--;

            if(m_attacktimer <= 0)
            {
                m_travelling = 1;
                m_attacktimer = Random.Range(100, 500);
                m_bossmove++;

            }
            m_transform.Translate(new Vector3(m_bossmove, 0, 0) * m_speed * Time.deltaTime, Space.World);

        }

       
    }

    void Avoid()
    {

        if (m_deviation >= 1)
        {
            if (m_path == 1)
            {
                m_transform.eulerAngles = m_startRotation + new Vector3(0, 45);
            }

            if (m_path == 2)
            {
                m_transform.eulerAngles = m_startRotation + new Vector3(0, -45);
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
            m_atbar = true;
        }

        if (m_trig.gameObject.tag == "Enemy_01" || m_trig.gameObject.tag == "Enemy_02")
        {
            m_travelling = 3;
            m_path = Random.Range(1, 3);
            m_deviation = 60f;
        }


    }

    public void ChangeHealth(float m_change)
    {
        m_health -= m_change;
    }
}


