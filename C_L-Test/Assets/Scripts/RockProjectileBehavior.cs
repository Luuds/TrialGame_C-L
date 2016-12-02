using UnityEngine;
using System.Collections;

public class RockProjectileBehavior : MonoBehaviour {

	void Start () {
		Destroy (gameObject, 10); // Destroys the projectile 10 seconds after it is created to avoid clutter
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player"){
			float damage = col.relativeVelocity.magnitude;
			Debug.Log ("Rock projectile dealt " + damage + " damage.");
			col.gameObject.SendMessage ("ApplyDamage", damage);
			Destroy (gameObject);
		}
	}

	public void ApplyDamage(float damage){
		Destroy (gameObject);
	}

	void Update () {
	
	}
}
