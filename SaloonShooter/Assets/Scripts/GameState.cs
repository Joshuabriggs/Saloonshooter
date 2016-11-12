using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    int m_score;
    public int m_level;
    int m_health = 50;

    //GameObjects
    [SerializeField] List<GameObject> m_enemyPrefabs = new List<GameObject>();

    //Lists
    List<EnemyMain> m_enemies = new List<EnemyMain>();

    //UI
    [SerializeField] Text m_levelDisplay;
    [SerializeField] Text m_scoreDisplay;

    public void AddScore(int _delta)
    {
        m_score += _delta;
        m_scoreDisplay.text = "Score: " + m_score;

    }

    public void AddLevel(int _delta)
    {
        m_level += _delta;
        m_levelDisplay.text = "Level: " + m_level;
    }

    public void SpawnEnemy(Vector3 _pos)
    {
        GameObject newEnemy = (GameObject)Instantiate(m_enemyPrefabs[Random.Range(0, m_enemyPrefabs.Count)], _pos, Quaternion.identity);
        m_enemies.Add(newEnemy.GetComponent<EnemyMain>());
    }

    public void DestroyEnemy(EnemyMain _enemy)
    {
        m_enemies.Remove(_enemy);
        Destroy(_enemy.gameObject);
    }

    public void HitEnemy(EnemyMain _enemy, float _damage)
    {
        _enemy.ChangeHealth(_damage);
    }

    public static GameState instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("A GameState instance already exists!");
        }
        instance = this;

    }
}
