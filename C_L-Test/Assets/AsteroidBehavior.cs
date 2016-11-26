using UnityEngine;
using System.Collections;

public class AsteroidBehavior : MonoBehaviour {


	SpriteRenderer spr; 
	Color newColour; 
	int particleCollisionCount; 
	GameObject TemObj;
	// Use this for initialization
	void Start () {

	}

	void OnParticleCollision (GameObject other){
		if (particleCollisionCount == 100)
			Destroy (gameObject); // You dead, asteroid. Took too much plasma to the face!
		if (Random.Range (0, 2) == 0) {
			// IT WAS A MONSTENR ALL A LONG. YOU MONSTER.
			// # Spawn monster dude here TODO #
			Destroy (gameObject);
		} else
			particleCollisionCount += 1; // We're burning plasma!
	}

	// Update is called once per frame
	void Update () {

	}
}
