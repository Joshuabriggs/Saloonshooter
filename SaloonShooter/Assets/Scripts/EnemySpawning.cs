using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawning : MonoBehaviour {

    List<Transform> m_spawnPoints = new List<Transform>();
    GameObject m_spawnGateCont;

    float m_spawnRate = 3f;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    // Use this for initialization
    void Start () {

        lastSpawnTime = Time.time;

        m_spawnGateCont = GameObject.Find("Spawn Gates");

        foreach(Transform child in m_spawnGateCont.transform)
        {
            m_spawnPoints.Add(child);
            Debug.Log("Added " + child.name + " to list of spawn gates.");
        }

        //StartCoroutine(SpawnEnemy());

	}

    void Update()
    {
        if (GameState.instance.m_wave < waves.Length)
        {
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[GameState.instance.m_wave].spawnInterval;
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
                 timeInterval > spawnInterval) && waves[GameState.instance.m_wave].enemiesInWave.Count > 0)
            {
                lastSpawnTime = Time.time;
                int rand = Random.Range(0, waves[GameState.instance.m_wave].enemiesInWave.Count);
                GameState.instance.SpawnEnemy((int)waves[GameState.instance.m_wave].enemiesInWave[rand], m_spawnPoints[Random.Range(0, m_spawnPoints.Count)].position);
                waves[GameState.instance.m_wave].enemiesInWave.RemoveAt(rand);
                enemiesSpawned++;
                Debug.Log(enemiesSpawned);
            }
            if (waves[GameState.instance.m_wave].enemiesInWave.Count <= 0 &&
                GameState.instance.m_enemies.Count <= 0)
            {
                GameState.instance.UpgradeMenu();
                //gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
        }
        else
        {
            //ALL THE WAVES ENDED DO SOMETHING HERE
        }
    }

    /*IEnumerator SpawnEnemy()
    {
        m_spawnRate = Mathf.Max(5f - (GameState.instance.m_level * 0.1f), 1f);
        Debug.Log("Spawn rate is set to " + m_spawnRate + " seconds.");
        yield return new WaitForSeconds(m_spawnRate);
        GameState.instance.SpawnEnemy(m_spawnPoints[Random.Range(0, m_spawnPoints.Count)].position);
        StartCoroutine(SpawnEnemy());
    }*/
}
