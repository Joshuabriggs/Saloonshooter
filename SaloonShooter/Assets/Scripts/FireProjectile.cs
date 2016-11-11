using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

    public GameObject Projectile;
    public Transform ProjectileSpawnPoint;

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            GameObject c_projectile = (GameObject)Instantiate(Projectile, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);   
        }
	}
}
