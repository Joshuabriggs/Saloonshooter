using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed = 100f;
    private Rigidbody rbody;

	// Use this for initialization
	void Start () {
        rbody = this.GetComponent<Rigidbody>();
        rbody.AddForce(transform.forward * ProjectileSpeed);
    }
}
