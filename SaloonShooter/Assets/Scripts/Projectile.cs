using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed = 100f;
    private Rigidbody rbody;
    public bool Spin = false;
    public bool isGlass = false;
    public AudioClip glassBreak;

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
                if (isGlass) {
                    AudioSource.PlayClipAtPoint(glassBreak, transform.position);
                }         
                onDestroy();
                break;
            case "Glass":
                Destroy(col.gameObject);
                AudioSource.PlayClipAtPoint(glassBreak, transform.position);
                onDestroy();                
                break;
            case "Enemy_01":
                if (col.gameObject.GetComponent<EnemyMain>() != null)
                {
                    GameState.instance.HitEnemy(col.gameObject.GetComponent<EnemyMain>(), m_damage);
                    if (isGlass)
                    {
                        AudioSource.PlayClipAtPoint(glassBreak, transform.position);
                    }
                }
                else
                {
                    Debug.LogError("Missing EnemyMain script on enemy! Errors may happen!");
                }
                onDestroy();
                break;
            case "Enemy_02":
                if (col.gameObject.GetComponent<EnemyMain>() != null)
                {
                    GameState.instance.HitEnemy(col.gameObject.GetComponent<EnemyMain>(), m_damage);
                    if (isGlass)
                    {
                        AudioSource.PlayClipAtPoint(glassBreak, transform.position);
                    }
                }
                else
                {
                    Debug.LogError("Missing EnemyMain script on enemy! Errors may happen!");
                }
                onDestroy();
                break;
            case "Enemy_Boss":
                if (col.gameObject.GetComponent<EnemyMain>() != null)
                {
                    GameState.instance.HitEnemy(col.gameObject.GetComponent<EnemyMain>(), m_damage);
                    if (isGlass)
                    {
                        AudioSource.PlayClipAtPoint(glassBreak, transform.position);
                    }
                }
                else
                {
                    Debug.LogError("Missing EnemyMain script on enemy! Errors may happen!");
                }
                onDestroy();
                break;
            case "Player":
                if (gameObject.tag == "Bottle")
                {
                    if (isGlass)
                    {
                        AudioSource.PlayClipAtPoint(glassBreak, transform.position);
                    }
                }
                else
                {
                    GameState.instance.UpdateHealth(-5f);
                }
                break;
        }
    }

        

            //Destroy(rbody);
    private void onDestroy() {
        Instantiate(particleEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
