using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    private GameObject m_player;
    private Vector3 m_relativePos;
    private Quaternion m_rotation;
    private Transform m_transform;
    private EnemyMain m_target;
    private float m_tempDistance;
    [SerializeField]
    private float m_attacktimer = 300f;
    [SerializeField]
    private GameObject m_bullet;

    // Use this for initialization
    void Start () {

        m_transform = transform;
        m_player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {

        m_tempDistance = 60f;
        foreach (EnemyMain _enemy in GameState.instance.m_enemies)
        {
            if (m_tempDistance > _enemy.m_transform.position.z - m_transform.position.z)
            {
                m_tempDistance = _enemy.m_transform.position.z - m_transform.position.z;
                m_target = _enemy;

            }

        }

        m_relativePos = m_target.transform.position - m_transform.position;
        m_rotation = Quaternion.LookRotation(m_relativePos);
        m_transform.rotation = m_rotation;
        
        if (m_attacktimer <= 0 )
        {
            Instantiate(m_bullet, m_transform.position, m_transform.rotation);
            m_attacktimer = Random.Range(200, 300);
        }

        m_attacktimer--;


        
            
        

    }
}
