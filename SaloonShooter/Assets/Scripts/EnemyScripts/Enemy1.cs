using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour {

    public int m_health = 10;
    private int m_path;
    [SerializeField]
    private float m_deviation = 60f;
    [SerializeField]
    private float m_speed = 5f;
    [SerializeField]
    private float m_attacktimer = 30f;
    private bool m_travelling;
    private Transform m_transform;
    private Rigidbody m_body;

    // Use this for initialization
    void Start () {

        m_transform = transform;
        m_path = Random.Range(1, 4);
        m_travelling = true;
        m_body = GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {

        if (m_travelling == true)
        {
            Moving();
        }

        else
        {
            Attacking();
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

        if(m_attacktimer <=0)
        {
            m_body.AddForce(new Vector3(0, 100f, 0));
            m_attacktimer = 30f;
        }

        m_transform.eulerAngles = new Vector3(0, 0, 0);

        m_attacktimer -= 1;

      
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
            m_travelling = false;
        }
    }
}
