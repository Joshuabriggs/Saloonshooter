﻿using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour {

    private float m_attacktimer = 30f;
    public bool m_run;
    private Transform m_transform;
    private Rigidbody m_body;

    // Use this for initialization
    void Start () {

        m_transform = transform;
        m_body = GetComponent<Rigidbody>();
        m_run = false;
	
	}

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<EnemyMain>().m_travelling == 2)
        {
            if (m_attacktimer <= 0)
            {
                m_body.AddForce(new Vector3(0, 100f, 0));
                m_attacktimer = 30f;
            }

            m_transform.eulerAngles = new Vector3(0, 0, 0);

            m_attacktimer -= 1;
        }
    }

}
