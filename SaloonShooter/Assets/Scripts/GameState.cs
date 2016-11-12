﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    int m_score;
    public int m_level;
    float m_health = 50;

    //GameObjects
    //[SerializeField] List<GameObject> m_enemyPrefabs = new List<GameObject>();

    //Lists
    List<EnemyMain> m_enemies = new List<EnemyMain>();

    //UI
    [SerializeField] Text m_levelDisplay;
    [SerializeField] Text m_scoreDisplay;
    [SerializeField] Image m_reloadBar;
    [SerializeField] Image m_healthBar;

    public void UpdateReloadBar(float _amount)
    {
        m_reloadBar.fillAmount = _amount;
    }

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
        //GameObject newEnemy = (GameObject)Instantiate(m_enemyPrefabs[Random.Range(0, m_enemyPrefabs.Count)], _pos, Quaternion.identity);
        //m_enemies.Add(newEnemy.GetComponent<EnemyMain>());
    }

    public void DestroyEnemy(EnemyMain _enemy)
    {
        m_enemies.Remove(_enemy);
        Destroy(_enemy.gameObject);

        AddScore(10);

    }

    public void PlayerHit(float _damage)
    {
        m_health -= _damage;
        m_healthBar.fillAmount = m_health / 50f;

    }

    public void HitEnemy(EnemyMain _enemy, float _damage)
    {
        _enemy.ChangeHealth(_damage);
        Debug.Log("Enemy was hit!");
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
