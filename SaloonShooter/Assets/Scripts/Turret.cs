using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    private GameObject m_player;
    private Vector3 m_relativePos;
    private Quaternion m_rotation;
    private Transform m_transform;

    // Use this for initialization
    void Start () {

        m_transform = transform;
	
	}
	
	// Update is called once per frame
	void Update () {

        m_relativePos = m_player.transform.position - m_transform.position;
        m_rotation = Quaternion.LookRotation(m_relativePos);
        m_transform.rotation = m_rotation;

    }
}
