using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

    public GameObject Projectile;
    public Transform ProjectileSpawnPoint;
    public float CooldownTime = 1f;
    private float currentTime;

    void Start() {
        currentTime = Time.time;
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            if (currentTime <=Time.time) {
                currentTime = Time.time + CooldownTime;
                GameObject c_projectile = (GameObject)Instantiate(Projectile, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);               
            }          
        }
	}
}
