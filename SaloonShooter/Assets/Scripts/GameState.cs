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
    public int m_beerNumber = 3;
    public bool m_revolver = false;
    public float m_playerSpeed = 8f;
    public int[] m_beerStorage;
    public GameObject m_sBeer;
    public GameObject m_mBeer;
    public GameObject m_lBeer;
    public GameObject m_oBeer;
    public GameObject m_turret;


    float deltaHealth = 0f;
    float healthBarSpeed = 6f;

    [SerializeField] Transform m_spawnGates;

    //GameObjects
    [SerializeField] List<GameObject> m_enemyPrefabs = new List<GameObject>();
    [SerializeField] GameObject m_door;

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

    public void BeerCreate(int _type)
    {
        for (int i = 0; i < 3; i++) {
            if (m_beerStorage[i] == 0)
            {
                m_beerStorage[i] = _type;
                i = 5;
            }
        }
        
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

        if(m_wave == 1)
        {
            AddANewDoor();
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>().m_drunkspin = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        

        for(int i = 0; i < 3; i++)
        {
           switch(m_beerStorage[i])
            {
                case 0:
                    break;
                case 1:

                    switch(i)
                    {
                        case 0:
                            Instantiate(m_sBeer, new Vector3(7, 1.3f, -5.5f), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(m_sBeer, new Vector3(0, 1.3f, -5.5f), Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(m_sBeer, new Vector3(-7, 1.3f, -5.5f), Quaternion.identity);
                            break;
                    }
                    break;
                case 2:

                    switch (i)
                    {
                        case 0:
                            Instantiate(m_mBeer, new Vector3(7, 1.3f, -5.5f), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(m_mBeer, new Vector3(0, 1.3f, -5.5f), Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(m_mBeer, new Vector3(-7, 1.3f, -5.5f), Quaternion.identity);
                            break;
                    }
                    break;
                case 3:

                    switch (i)
                    {
                        case 0:
                            Instantiate(m_lBeer, new Vector3(7, 1.3f, -5.5f), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(m_lBeer, new Vector3(0, 1.3f, -5.5f), Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(m_lBeer, new Vector3(-7, 1.3f, -5.5f), Quaternion.identity);
                            break;
                    }
                    break;
                case 4:

                    switch (i)
                    {
                        case 0:
                            Instantiate(m_oBeer, new Vector3(7, 1.3f, -5.5f), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(m_oBeer, new Vector3(0, 1.3f, -5.5f), Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(m_oBeer, new Vector3(-7, 1.3f, -5.5f), Quaternion.identity);
                            break;
                    }
                    break;

            }
        }

    }

    void AddANewDoor()
    {
        GameObject newDoor = Instantiate(m_door);
        newDoor.transform.parent = m_spawnGates.transform;
        GetComponent<EnemySpawning>().UpdateSpawnGates();
        Debug.Log("NEW DOOR");
    }

    public void SpawnEnemy(int _type, Vector3 _pos)
    {
        GameObject newEnemy = (GameObject)Instantiate(m_enemyPrefabs[_type], _pos, Quaternion.identity);
        m_enemies.Add(newEnemy.GetComponent<EnemyMain>());
    }

    public void DestroyEnemy(EnemyMain _enemy, bool _killed)
    {
        m_enemies.Remove(_enemy);
        Destroy(_enemy.gameObject);

        if(_killed)
        {
            AddScore(10);
        }
        

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
        m_beerStorage = new int[3];
        m_beerStorage[0] = 2;
        m_beerStorage[1] = 2;
        m_beerStorage[2] = 2;


    }
}
