using UnityEngine;
using System.Collections;

public class RockProjectileBehavior : MonoBehaviour {

	void Start () {
		Destroy (gameObject, 10); // Destroys the projectile 10 seconds after it is created to avoid clutter
	}

	void OnCollissionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player"){
			Debug.Log ("Rock projectile hit.");
			Destroy (gameObject);
		}
	}

	void Update () {
	
	}
}
