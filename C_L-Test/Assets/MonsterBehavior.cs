using UnityEngine;
using System.Collections;


/*
 *  ######## THIS SCRIPT IS NO LONGER USED ############
 *  Everything happens in the "AsteroidBehavior" script.
 * 
 */

public class MonsterBehavior : MonoBehaviour {

	public Transform aggroArea;
	public LayerMask aggroLayer;
	float aggroRadius = 20f;
	bool enemiesAbout;

	GameObject aggroTarget;

	int particleCollisionCount; 


	void Start () {
		aggroTarget = GameObject.Find ("ShipPrefab");
	}

	void OnParticleCollision (GameObject other){
		if (particleCollisionCount == 150)
			Destroy (gameObject); // You dead, baddie. Took too much plasma to the face!
		else
			particleCollisionCount += 1; // We're burning plasma!
	}
		
	void Update () {

		//Move towards-ish the player if it is near

		if (Vector2.Distance (gameObject.transform.position, aggroTarget.transform.position) < aggroRadius) {
			Vector2 viktor = new Vector2 (aggroTarget.transform.position.x - transform.position.x, aggroTarget.transform.position.y - transform.position.y);
			viktor *= 1f + (Random.Range (-5f, 6f) * 0.1f); // Randomizes the vector a bit so monster doesn't run straight at the player
			GetComponent<Rigidbody2D> ().AddForce (viktor * 0.5f);

			/* Debug outputs
			Debug.Log (transform.position);
			Debug.Log (aggroTarget.transform.position);
			Debug.Log (viktor);
			*/
		}
			
		//Being shot at by particles

	}
}