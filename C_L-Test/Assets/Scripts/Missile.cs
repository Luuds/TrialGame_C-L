using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public Collider2D target;
	int activationPeriod = 25;
	int activationCounter = 0;

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		if (activationCounter > activationPeriod) { // Doesn't collide with other missiles or anything while "launching".
			if (col.gameObject.layer == 9 || col.gameObject.layer == 10) {
				Vector2 relativePos = col.gameObject.transform.position - transform.position;
				relativePos = relativePos.normalized;
				relativePos = relativePos * 500f;
				col.rigidbody.AddForce (relativePos);
				col.gameObject.SendMessage ("ApplyDamage", Random.Range (40, 60));
			}
			Explode();
		}

	}

	void Explode(){
		Destroy (gameObject);
		Instantiate (Resources.Load ("GenericExplosion"), transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		activationCounter += 1;
		if (activationCounter == activationPeriod) {
			ParticleSystem exhaust = GetComponentInChildren<ParticleSystem> ();
			exhaust.Play ();
		}
		if (activationCounter > activationPeriod) {
			if (target != null) {
				Vector3 relativePos = target.gameObject.transform.position - transform.position;
				relativePos = relativePos.normalized * 1f;
				GetComponent<Rigidbody2D> ().AddForce (relativePos);

				float angle = (Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg) - 90;
				Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
				transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * 1f);
			} else
				Explode ();
		}
	}
}
