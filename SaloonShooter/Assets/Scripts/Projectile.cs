using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed = 100f;
    private Rigidbody rbody;
    public bool Spin = false;

    public GameObject particleEffect;

    float m_damage = 10f;

	// Use this for initialization
	void Start () {
        rbody = this.GetComponent<Rigidbody>();
        rbody.AddForce(transform.forward * ProjectileSpeed);
    }

    void Update() {
        if (Spin) {
            rbody.AddTorque(transform.right*3000f*Time.deltaTime);
            //transform.Rotate(1000 * Time.deltaTime,0, 0);
        }
    }

    void OnCollisionEnter(Collision col) {
        switch (col.gameObject.tag)
        {
            case "Ground":
                Spin = false;
                onDestroy();
                break;

            case "Enemy":
                if (col.gameObject.GetComponent<EnemyMain>() != null)
                {
                    GameState.instance.HitEnemy(col.gameObject.GetComponent<EnemyMain>(), m_damage);
                }
                else
                {
                    Debug.LogError("Missing EnemyMain script on enemy! Errors may happen!");
                }
                onDestroy();
                break;

            case "Player":
                GameState.instance.UpdateHealth(-5f);
                break;
        }
    }

        

            //Destroy(rbody);
    private void onDestroy() {
        Instantiate(particleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
