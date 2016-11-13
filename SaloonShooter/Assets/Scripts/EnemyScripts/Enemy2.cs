﻿using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour {

    private float m_attacktimer = 30f;
    private Vector3 m_shotSpawn;
    private Transform m_transform;
    public Transform m_guntransform;
    private Rigidbody m_body;
    private GameObject m_player;
    public GameObject m_bullet;
    public bool m_run;
    public GameObject m_SmokeFX;

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

        m_shotSpawn = m_guntransform.position + new Vector3(0, 0, 0);

        if (GetComponent<EnemyMain>().m_travelling == 2)
        {
           
            if (m_attacktimer <= 0)
            {
                m_attacktimer = 200f;
                Instantiate(m_bullet, m_shotSpawn, m_guntransform.rotation);
                Instantiate(m_SmokeFX, m_guntransform.position, m_guntransform.rotation);
            }

            m_attacktimer -= 1;


        }


    }



}
