using UnityEngine;
using System.Collections;

public class EnemyBulletScript : MonoBehaviour {

    [SerializeField]
    private float m_speed = 20f;
    private Transform m_transform;

	// Use this for initialization
	void Start () {

        m_transform = transform;
	
	}
	
	// Update is called once per frame
	void Update () {

        m_transform.Translate(new Vector3(0, 0, 1) * m_speed * Time.deltaTime);
	}
}
