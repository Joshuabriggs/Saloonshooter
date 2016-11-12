using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

    public GameObject Projectile;
    public Transform ProjectileSpawnPoint;
    
    private float timeAtReload;

    void Start() {
        timeAtReload = Time.time;
    }

	// Update is called once per frame
	void Update () {

        GameState.instance.UpdateReloadBar((Time.time - timeAtReload) / GameState.instance.reloadTime);
        Debug.Log((Time.time - timeAtReload) / GameState.instance.reloadTime);

        if (Input.GetMouseButtonDown(0)) {
            if (timeAtReload <= Time.time - GameState.instance.reloadTime) {
                timeAtReload = Time.time;
                GameObject c_projectile = (GameObject)Instantiate(Projectile, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);               
            }          
        }
	}
}
