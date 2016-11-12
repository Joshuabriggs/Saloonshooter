using UnityEngine;
using System.Collections;

public class EnemyBoss : MonoBehaviour
{

	private float m_attacktimer = 30f;
    private Vector3 m_shotSpawn;
    private Transform m_transform;
    private Transform m_guntransform;
    private bool m_isdead = false;
    private Rigidbody m_body;
    private GameObject m_player;
    public GameObject m_bullet;
    public bool m_run;
    private int m_deathtimer = 100;


    // Use this for initialization
    void Start()
    {

        m_transform = transform;
        m_body = GetComponent<Rigidbody>();
        m_guntransform = GetComponentInChildren<Transform>();
        m_player = GameObject.Find("PlayerBody");
        m_run = false;


    }

    // Update is called once per frame
    void Update()
    {

        m_shotSpawn = m_guntransform.position + new Vector3(0, 0, -1);

        if (GetComponent<EnemyMain>().m_travelling == 2)
        {

            if (m_attacktimer <= 0)
            {
                m_attacktimer = 50f;
                Instantiate(m_bullet, m_shotSpawn, m_transform.rotation);
            }

            m_attacktimer -= 1;

            if (m_isdead == false)
            {
                if (GetComponent<EnemyMain>().m_atbar == true)
                {
                
                        m_body.AddForce(new Vector3(0, 500f, 500f));
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
                    DestroyObject(gameObject);
                }
            }


        }


    }



}
