using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    public int m_score;
    public int m_wave;

    //Player stats
    float m_health = 50;
    public float m_maxHealth = 50f;
    public float reloadTime = 1f;
    public float m_rRealoadTime = 5f;
    public int m_currentWeapon = 1;
    public int m_shotCount = 6;
    public int m_turretCount = 0;
    public bool m_revolver = false;
    public GameObject m_turret;


    float deltaHealth = 0f;
    float healthBarSpeed = 6f;

    //GameObjects
    [SerializeField] List<GameObject> m_enemyPrefabs = new List<GameObject>();

    //Lists
    public List<EnemyMain> m_enemies = new List<EnemyMain>();

    //UI
    [SerializeField] Text m_waveDisplay;
    [SerializeField] Text m_scoreDisplay;
    [SerializeField] Image m_reloadBar;
    [SerializeField] Image m_healthBar;
    [SerializeField] Text m_healthText;
    [SerializeField] Text m_maxHealthText;

    [SerializeField] Canvas m_mainUI;
    [SerializeField] Canvas m_deadUI;


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
                deltaHealth -= Time.deltaTime * healthBarSpeed;
                UpdateHealthUI();
            }
            else
            {
                m_health += deltaHealth;
                deltaHealth = 0;
                UpdateHealthUI();
            }

            m_health = Mathf.Max(0, m_health);
            m_health = Mathf.Min(m_maxHealth, m_health);

        }

        if(m_health <= 0)
        {
            m_mainUI.gameObject.SetActive(false);
            m_deadUI.gameObject.SetActive(true);
        }

    }

    

    public void UpdateReloadBar(float _amount)
    {
        m_reloadBar.fillAmount = _amount;
        
    }

    public void TurretCreate()
    {
        switch(m_turretCount)
        {
            case 0:
                Instantiate(m_turret, new Vector3(9, 10, -6), Quaternion.identity);
                    break;

            case 1:
                Instantiate(m_turret, new Vector3(-9, 10, -6), Quaternion.identity);
                break;
        }

        m_turretCount++;
    }

    public void UpdateHealthUI()
    {
        m_healthBar.fillAmount = m_health / m_maxHealth;
        m_healthText.text = (int)m_health + "";
        m_maxHealthText.text = "/" + m_maxHealth;

        if(m_health >= m_maxHealth - (m_maxHealth * 0.5))
        {
            m_healthBar.color = Color.green;
        }
        else if(m_health >= m_maxHealth - (m_maxHealth * 0.8))
        {
            m_healthBar.color = new Color(1f, 0.6f, 0);
        }
        else
        {
            m_healthBar.color = Color.red;
        }

    }

    public void AddScore(int _delta)
    {
        m_score += _delta;
        m_scoreDisplay.text = "$" + m_score;
        
    }

    public void UpgradeMenu()
    {
        Time.timeScale = 0f;
        if(SceneManager.sceneCount <= 1)
        {
            SceneManager.LoadScene("UpgradeMenu", LoadSceneMode.Additive);
        }
        
    }

    public void NextWave()
    {
        Time.timeScale = 1f;
        m_wave ++;
        m_waveDisplay.text = "Wave: " + (m_wave+1);
    }

    public void SpawnEnemy(int _type, Vector3 _pos)
    {
        GameObject newEnemy = (GameObject)Instantiate(m_enemyPrefabs[_type], _pos, Quaternion.identity);
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
