using UnityEngine;
using System.Collections;

public class EnemyBoss : MonoBehaviour
{

    private float m_attacktimer = 30f;
    private Vector3 m_shotSpawn;
    private Transform m_transform;
    public Transform m_guntransform;
    private bool m_isdead = false;
    private Rigidbody m_body;
    private GameObject m_player;
    public GameObject m_bullet;
    public bool m_run;
    private int m_deathtimer = 100;
    public GameObject m_boom;


    // Use this for initialization
    void Start()
    {

        m_transform = transform;
        m_body = GetComponent<Rigidbody>();
        m_player = GameObject.Find("PlayerBody");
        m_run = false;


    }

    // Update is called once per frame
    void Update()
    {

        m_shotSpawn = m_guntransform.position;

        if (GetComponent<EnemyMain>().m_travelling == 2)
        {

            if (m_attacktimer <= 0)
            {
                m_attacktimer = 50f;
                Instantiate(m_bullet, m_shotSpawn, m_guntransform.rotation);
            }

            m_attacktimer -= 1;

            if (m_isdead == false)
            {
                if (GetComponent<EnemyMain>().m_atbar == true)
                {

                    Instantiate(m_boom, m_transform.position + new Vector3(0, 3), Quaternion.identity);
                    m_body.AddForce(new Vector3(0, 5000f, 5000f));
                    m_isdead = true;
                    GameState.instance.UpdateHealth(-40f);


                    m_transform.eulerAngles = new Vector3(0, 0, 0);

                    m_attacktimer -= 1;
                }
            }

            else
            {
                m_deathtimer--;

                if (m_deathtimer <= 0)
                {
                    GameState.instance.DestroyEnemy(gameObject.GetComponent<EnemyMain>(), false);
                }
            }


        }


    }



}
