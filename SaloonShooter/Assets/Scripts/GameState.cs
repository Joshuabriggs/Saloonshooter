using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    int m_score;
    public int m_level;

    //GameObjects
    [SerializeField] GameObject m_enemy;

    //Lists
    List<Enemy1> m_enemies = new List<Enemy1>();

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
        GameObject newEnemy = (GameObject)Instantiate(m_enemy, _pos, Quaternion.identity);
        m_enemies.Add(newEnemy.GetComponent<Enemy1>());
    }

    public void DestroyEnemy(Enemy1 _enemy)
    {
        m_enemies.Remove(_enemy);
        Destroy(_enemy.gameObject);
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
