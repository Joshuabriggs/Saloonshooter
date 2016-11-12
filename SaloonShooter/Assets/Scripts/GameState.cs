using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    int m_score;
    public int m_wave;
    float m_health = 50;
    float m_maxHealth = 50f;

    float deltaHealth = 0f;
    float healthBarSpeed = 6f;

    //GameObjects
    [SerializeField] List<GameObject> m_enemyPrefabs = new List<GameObject>();

    //Lists
    List<EnemyMain> m_enemies = new List<EnemyMain>();

    //UI
    [SerializeField] Text m_waveDisplay;
    [SerializeField] Text m_scoreDisplay;
    [SerializeField] Image m_reloadBar;
    [SerializeField] Image m_healthBar;
    [SerializeField] Text m_healthText;


    void Update()
    {
        if(deltaHealth != 0)
        {
            //deplete health
            if (deltaHealth < -Time.deltaTime * healthBarSpeed)
            {
                m_health -= Time.deltaTime * healthBarSpeed;
                deltaHealth += Time.deltaTime * healthBarSpeed;
                UpdateHealthUI();
            }

            //gain health
            else if (deltaHealth > Time.deltaTime * healthBarSpeed)
            {
                m_health += Time.deltaTime * healthBarSpeed;
                deltaHealth += Time.deltaTime * healthBarSpeed;
                UpdateHealthUI();
            }
            else
            {
                m_health += deltaHealth;
                deltaHealth = 0;
                UpdateHealthUI();
            }
        }

    }

    public void UpdateReloadBar(float _amount)
    {
        m_reloadBar.fillAmount = _amount;
        
    }

    public void UpdateHealthUI()
    {
        m_healthBar.fillAmount = m_health / 50f;
        m_healthText.text = "" + (int)m_health;

        if(m_health >= m_maxHealth / 4 + m_maxHealth / 2)
        {
            //m_healthBar.material.color = Color.red;
        }

    }

    public void AddScore(int _delta)
    {
        m_score += _delta;
        m_scoreDisplay.text = "Score: " + m_score;
        
    }

    public void UpgradeMenu()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("UpgradeMenu", LoadSceneMode.Additive);
    }

    public void NextWave()
    {
        Time.timeScale = 1f;
        m_wave ++;
        m_waveDisplay.text = "Wave: " + (m_wave+1);
    }

    public void SpawnEnemy(int _type, Vector3 _pos)
    {
        GameObject newEnemy = (GameObject)Instantiate(m_enemyPrefabs[Random.Range(0, m_enemyPrefabs.Count)], _pos, Quaternion.identity);
        m_enemies.Add(newEnemy.GetComponent<EnemyMain>());
    }

    public void DestroyEnemy(EnemyMain _enemy)
    {
        m_enemies.Remove(_enemy);
        Destroy(_enemy.gameObject);

        AddScore(10);

    }

    public void UpdateHealth(float _delta)
    {

        deltaHealth += _delta;

        //Old health system
        /*m_health += _delta;
        m_healthBar.fillAmount = m_health / 50f;
        Debug.Log(m_health);*/

        

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
