using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour {

    private float m_attacktimer = 30f;
    public bool m_run;
    private Transform m_transform;
    private Rigidbody m_body;
    private bool m_isdead;
    private float m_deathtimer;

    // Use this for initialization
    void Start () {

        m_transform = transform;
        m_body = GetComponent<Rigidbody>();
        m_run = false;
        m_isdead = false;
        m_deathtimer = 100f;
	
	}

    // Update is called once per frame
    void Update()
    {

        if (m_isdead == false)
        {
            if (GetComponent<EnemyMain>().m_travelling == 2)
            {
                if (m_attacktimer <= 0)
                {
                    m_body.AddForce(new Vector3(0, 500f, 500f));
                    m_attacktimer = 90f;
                    m_isdead = true;
                    GameState.instance.UpdateHealth(-10f);
                }

                m_transform.eulerAngles = new Vector3(0, 0, 0);

                m_attacktimer -= 1;
            }
        }

        else
        {
            m_deathtimer--;

            if (m_deathtimer <= 0)
            {
                GameState.instance.DestroyEnemy(gameObject.GetComponent<EnemyMain>());
            }
        }
    }

}
