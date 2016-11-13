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
    private float reloadVal;
    private bool isReloading = false;

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
                //Debug.Log((Time.time - timeAtReload) / GameState.instance.reloadTime);
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
                reloadVal = (Time.time - timeAtReload) / GameState.instance.m_rRealoadTime;
                m_revolver.GetComponent<Animator>().enabled = true;
                GameState.instance.UpdateReloadBar(reloadVal);
                //Debug.Log((Time.time - timeAtReload) / GameState.instance.m_rRealoadTime);
                Projectile = rProjectile;
                if (Input.GetMouseButtonDown(0) && !isReloading && GameState.instance.m_shotCount > 0)
                {
                    if (GameState.instance.m_shotCount > 0)
                    {
                        GameObject c_projectile = (GameObject)Instantiate(Projectile, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
                        GameState.instance.m_shotCount--;
                    }                   
                }

                Debug.Log("ReloadBar:" + reloadVal);
                if (!isReloading && GameState.instance.m_shotCount<1)
                {
                    timeAtReload = Time.time;
                    reloadVal = 0;
                    isReloading = true;
                }
                if(reloadVal >1 && isReloading)
                {
                    GameState.instance.m_shotCount = 6;
                    isReloading = false;
                }
                break;
        }
	}
}
