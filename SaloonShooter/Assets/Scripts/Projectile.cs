using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed = 100f;
    private Rigidbody rbody;
    public bool Spin = false;

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
        if(col.gameObject.tag == "Ground"){
            Spin = false;
            //Destroy(rbody);
        }
    }
}
