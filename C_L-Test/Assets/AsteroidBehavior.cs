using UnityEngine;
using System.Collections;

public class AsteroidBehavior : MonoBehaviour {

	// Generic stuff
	public float hitPoints = 100;
	public float quantumProbability = 50; // Likelihood of the asteroid actually being a monster (0 - 100)
	bool quantedStateObserved;
	int intervalCounter = 0;
	Animator anim;
	int moveHash = Animator.StringToHash ("Move");

	// Monster stuff
	bool isMonster = false;
	int attackType;
	GameObject aggroTarget;
	float aggroRadius = 30f;
	int throwCooldown;


	void Start () {
		hitPoints = Random.Range (75, 100);
		quantumProbability = Random.Range (0, 100);
		gameObject.GetComponent<Rigidbody2D> ().drag = 0.1f;
		quantedStateObserved = false;
		isMonster = false;
		attackType = Random.Range (0, 2); // 0 = Ranged, 1 = Melee attack
		//attackType = 0; // For testing purposes always ranged

		anim = GetComponent<Animator> ();

	}

	void RespawnAsteroid(){
		Quaternion quark = Random.rotation;
		Vector2 viktor = new Vector2 (30,30);
		viktor = quark * viktor;
		Start(); // Resets all the properties before cloning
		GameObject clone = (GameObject) Instantiate(gameObject, viktor, transform.rotation); // Create a new asteroid at a randomly rotated vector.
		Vector2 viktoria = new Vector2(Random.Range(-20f,20f) - clone.transform.position.x, Random.Range(-20f,20f) - clone.transform.position.y);
		clone.GetComponent<Rigidbody2D>().velocity = (viktoria * 0.15f);
		clone.GetComponent<Animator> ().SetBool (moveHash, false);
		DeathFX ();
		Destroy (gameObject);
	}

	void OnParticleCollision (GameObject other){ // This shit happens when it gets hit by filthy plasma particles.
		// First, check if it's about to die.
		if (hitPoints <= 0)	// You dead, asteroid. Took too much plasma to the face!
			RespawnAsteroid();
		// If it didn't die, check if a monster lurks within it!
		if (!quantedStateObserved && !isMonster){
			quantedStateObserved = true;
			if (quantumProbability > 30f) { // IT WAS A MONSTER ALL A LONG. YOU MONSTER. Quantum physics is a bitch.
				// Transform into a monster!
				isMonster = true;

				hitPoints = Random.Range (100, 125);
				gameObject.GetComponent<Rigidbody2D> ().drag = 1.5f;
				Debug.Log ("It was a monster all along!");

			}
		} else{
			hitPoints -= 1; // Die asteroid/monster, die! Reveal to me your bounty.
			Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
			Vector2 direction = gameObject.transform.position - other.transform.position;
			direction = direction.normalized;
			body.AddForce (direction * 10f);
		}
			
	}

	void OutOfBounds(GameObject peikko){
		// Kill asteroids that are too far away.
		if ((-75f >= peikko.transform.position.x) || (75f <= peikko.transform.position.x) || (-75f >= peikko.transform.position.y) || (75f <= peikko.transform.position.y)) {
			Debug.Log ("There is no escape!");
			RespawnAsteroid ();
		}
	}

	void DeathFX(){
		GameObject deathFX;
		if (gameObject.CompareTag ("Green")) {
			Debug.Log ("Green explosion");
			deathFX = (GameObject)Instantiate (Resources.Load ("RockBurst1"), new Vector2 (transform.position.x, transform.position.y), transform.rotation);
		}
		if (gameObject.CompareTag ("Red")) {
			Debug.Log ("Red explosion");
			deathFX = (GameObject)Instantiate (Resources.Load ("RockBurst2"), new Vector2 (transform.position.x, transform.position.y), transform.rotation);
		}
		if (gameObject.CompareTag ("Yellow")) {
			Debug.Log ("Yellow explosion");
			deathFX = (GameObject)Instantiate (Resources.Load ("RockBurst3"), new Vector2 (transform.position.x, transform.position.y), transform.rotation);
		}
		
	}
		
	void ThrowRock(GameObject source, GameObject target) {
		GameObject projectile = (GameObject) Instantiate(Resources.Load("RockProjectilePrefab"), new Vector2(source.transform.position.x, source.transform.position.y), source.transform.rotation); // Create a new asteroid at a randomly rotated vector.
		Vector2 viktoria = new Vector2(target.transform.position.x - source.transform.position.x, target.transform.position.y - source.transform.position.y);
		viktoria = viktoria.normalized;
		Vector2 wolfgang = target.transform.position.normalized;
		viktoria.x = viktoria.x - wolfgang.y;
		viktoria.y = viktoria.y - wolfgang.y;
		viktoria = viktoria * 15.0f;
		projectile.GetComponent<Rigidbody2D>().velocity = (viktoria);
	}


	void Update () {
		anim.SetBool (moveHash, isMonster);
		if (isMonster){
			// Find the player and set it as a target if necessary. Replace with something more efficient later.
			if (aggroTarget != null){
				
				// While player is close enough, go after it with the vengeance of a thousand grumpy suns.
				if (Vector2.Distance (transform.position, aggroTarget.transform.position) <= aggroRadius) {

					Vector3 relativePos = aggroTarget.transform.position - gameObject.transform.position;

					if (attackType == 0) { // Those who use ranged attacks keep their distance and flank the player
						intervalCounter += 1;
						throwCooldown += 1;

						Vector3 vectorToTarget = relativePos;
						float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
						Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
						transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);

						if (intervalCounter >= 10) {
							Vector2 viktor = new Vector2 (Random.Range (1f, 2f) * aggroTarget.transform.position.x - gameObject.transform.position.x, Random.Range (1f, 2f) * aggroTarget.transform.position.y - gameObject.transform.position.y);
							gameObject.GetComponent<Rigidbody2D> ().AddForce (viktor * 0.25f);
							intervalCounter = 0; // Don't want to update velocity every cycle
							if (throwCooldown >= 180) {
								anim.SetTrigger ("Attack");
								ThrowRock (gameObject, aggroTarget);
								throwCooldown = 0; // Need time to burp up another rock
							}
						}
					}

					if (attackType == 1) { // Those who use melee attacks charge the player with reckless abandon
						intervalCounter += 1;

						if (intervalCounter >= 150) {
							Vector2 viktor = new Vector2 (aggroTarget.transform.position.x - transform.position.x, aggroTarget.transform.position.y - transform.position.y);
							viktor = viktor.normalized * Random.Range (25f, 40f);
							gameObject.GetComponent<Rigidbody2D> ().velocity = (viktor);
							anim.SetTrigger ("Attack");
							intervalCounter = 0; // Don't want to update velocity every cycle
						}
					}
					
				} else { // Can't find the player -> return to a life of peace and quiet. AKA reset.
					Start ();
				}
			} else
				aggroTarget = GameObject.Find ("Engine");
		}
		OutOfBounds (gameObject); // Checks if the asteroid has drifted too far away.

	}
}
