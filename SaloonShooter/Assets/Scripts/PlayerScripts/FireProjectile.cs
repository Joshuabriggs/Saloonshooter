using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

    public GameObject bProjectile;
    public GameObject rProjectile;
    public GameObject m_revolver;
    public GameObject m_bottle;
    private GameObject Projectile;
    public Transform ProjectileSpawnPoint;
    [HideInInspector]
    public int m_weapon;
    
    private float timeAtReload;

    void Start() {
        timeAtReload = Time.time;
    }

	// Update is called once per frame
	void Update () {

        
        
        if(GameState.instance.m_revolver == false)
        {
            GameState.instance.m_currentWeapon = 1;
        }
        switch(GameState.instance.m_currentWeapon)
        {
            case 1:

                m_revolver.SetActive(false);
                m_bottle.SetActive(true);
                GameState.instance.UpdateReloadBar((Time.time - timeAtReload) / GameState.instance.reloadTime);
                Debug.Log((Time.time - timeAtReload) / GameState.instance.reloadTime);
                Projectile = bProjectile;
                if (Input.GetMouseButtonDown(0))
                {
                    if (timeAtReload <= Time.time - GameState.instance.reloadTime)
                    {
                        timeAtReload = Time.time;
                        GameObject c_projectile = (GameObject)Instantiate(Projectile, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
                    }
                }
                break;

            case 2:

                m_revolver.SetActive(true);
                m_bottle.SetActive(false);
                m_revolver.GetComponent<Animator>().enabled = true;
                GameState.instance.UpdateReloadBar((Time.time - timeAtReload) / GameState.instance.m_rRealoadTime);
                Debug.Log((Time.time - timeAtReload) / GameState.instance.m_rRealoadTime);
                Projectile = rProjectile;
                if (Input.GetMouseButtonDown(0))
                {
                    if (GameState.instance.m_shotCount > 0)
                    {
                        GameObject c_projectile = (GameObject)Instantiate(Projectile, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
                        GameState.instance.m_shotCount--;
                    }

                    else
                    {
                        if (timeAtReload <= Time.time - GameState.instance.m_rRealoadTime)
                        {
                            
                            timeAtReload = Time.time;
                            GameState.instance.m_shotCount = 6;

                        }
                    }
                    
                }

                break;
        }
	}
}
