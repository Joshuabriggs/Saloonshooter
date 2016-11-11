using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawning : MonoBehaviour {

    List<Transform> m_spawnPoints = new List<Transform>();
    GameObject m_spawnGateCont;

    float m_spawnRate = 3f;

    [SerializeField] GameObject m_enemy;

	// Use this for initialization
	void Start () {

        m_spawnGateCont = GameObject.Find("Spawn Gates");

        foreach(Transform child in m_spawnGateCont.transform)
        {
            m_spawnPoints.Add(child);
            Debug.Log("Added " + child.name + " to list of spawn gates.");
        }

        StartCoroutine(SpawnEnemy());

	}
	
	IEnumerator SpawnEnemy()
    {

        yield return new WaitForSeconds(m_spawnRate);
        Instantiate(m_enemy, m_spawnPoints[Random.Range(0, m_spawnPoints.Count)].position, Quaternion.identity);
        StartCoroutine(SpawnEnemy());
    }
}
