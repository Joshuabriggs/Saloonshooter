using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed = 100f;
    private Rigidbody rbody;
    public bool Spin = false;

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
        switch (col.gameObject.tag) {
            case "Ground":
                Spin = false;
                Destroy(gameObject);
                break;

            case "Enemy":
                if(col.gameObject.GetComponent<EnemyMain>() != null)
                {
                    GameState.instance.HitEnemy(col.gameObject.GetComponent<EnemyMain>(), m_damage);
                }
                else
                {
                    Debug.LogError("Missing EnemyMain script on enemy! Errors may happen!");
                }
                Destroy(gameObject);
                break;

            case "Player":
                GameState.instance.PlayerHit(5f);
                break;
    }

        

            //Destroy(rbody);
        }
    }
