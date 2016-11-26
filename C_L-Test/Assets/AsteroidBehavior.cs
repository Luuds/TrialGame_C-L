using UnityEngine;
using System.Collections;

public class AsteroidBehavior : MonoBehaviour {

	public float hitPoints = 100;
	public float quantumProbability = 50; // Likelihood of the asteroid actually being a monster (0 - 100)
	bool quantedStateObserved;
	SpriteRenderer spr; 
	Color newColour; 
	int particleCollisionCount; 
	GameObject TemObj;


	// Use this for initialization
	void Start () {
		hitPoints = Random.Range (100, 200);
		quantumProbability = Random.Range (0, 100);
		quantedStateObserved = false;
	}

	void OnParticleCollision (GameObject other){
		if (hitPoints <= 0) {	// You dead, asteroid. Took too much plasma to the face!
			Quaternion quark = Random.rotation;
			Vector2 viktor = new Vector2 (30,30);
			viktor = quark * viktor;
			Start(); // Resets all the properties before cloning
			Rigidbody2D clone = (Rigidbody2D) Instantiate(gameObject, viktor, transform.rotation); // Create a new asteroid at a randomly rotated vector.
			Destroy (gameObject);
			Vector2 viktoria = new Vector2(Random.Range(-10f,10f) - clone.transform.position.x, Random.Range(-10f,10f) - clone.transform.position.y);
			Debug.Log (viktoria);
			clone.velocity = (viktoria * 100);

		}
		if (!quantedStateObserved){ // Now we shall see where your loyalties lie...
			quantedStateObserved = true;
			if (Random.Range (0f, 100f) < quantumProbability) { // IT WAS A MONSTENR ALL A LONG. YOU MONSTER. Quantum physics is a bitch.
				// # Spawn monster dude here TODO #
				Destroy (gameObject);
				Debug.Log ("It was a monster all along!");
			}
		} else
			hitPoints -= 1; // Die asteroid, die! Reveal to me your bounty.
	}

	// Update is called once per frame
	void Update () {

	}
}
