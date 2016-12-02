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
	GameObject leash;

	// Monster stuff
	bool isMonster = false;
	int attackType;
	GameObject aggroTarget;
	float aggroRadius = 40f;
	int throwCooldown;


	void Start () {
		hitPoints = Random.Range (75, 100);
		quantumProbability = Random.Range (0, 100);
		gameObject.GetComponent<Rigidbody2D> ().drag = 0.1f;
		quantedStateObserved = false;
		isMonster = false;
		attackType = Random.Range (0, 2); // 0 = Ranged, 1 = Melee attack
		leash = GameObject.FindGameObjectWithTag("Player");
		//attackType = 0; // For testing purposes always ranged

		anim = GetComponent<Animator> ();

	}

	void RespawnAsteroid(){
		Vector2 playerVector = leash.transform.position;
		Quaternion quark = Random.rotation;
		Vector2 farAway = new Vector2 (40,0);
		Vector2 blob = quark * farAway;
		Vector2 fuk = playerVector + blob;
		Start(); // Resets all the properties before cloning
		GameObject clone = (GameObject) Instantiate(gameObject, fuk, transform.rotation); // Create a new asteroid at a randomly rotated vector.
		Vector2 shove = playerVector - fuk;
		shove = shove.normalized;
		clone.GetComponent<Rigidbody2D>().velocity = shove;
		clone.GetComponent<Animator> ().SetBool (moveHash, false);
		DeathFX ();
		Destroy (gameObject);
	}

	void OnParticleCollision (GameObject other){
		
		if (other.CompareTag ("PlasmaTorch")) {
			Rigidbody2D body = gameObject.GetComponent<Rigidbody2D> ();
			Vector2 direction = gameObject.transform.position - other.transform.position;
			direction = direction.normalized;
			body.AddForce (direction * 10f);
			SendMessage ("ApplyDamage", Random.Range (1.0f, 1.25f));
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player"){
			float damage = col.relativeVelocity.magnitude;
			Debug.Log ("Asteroid dealt and took " + damage + " damage.");

			// Both take damage from the collision
			col.gameObject.SendMessage ("ApplyDamage", damage);
			SendMessage ("ApplyDamage", damage);
		}
	}

	public void ApplyDamage(float damage){
		hitPoints -= damage;
		if (hitPoints <= 0) {
			PlayerBehavior.resourcesGathered += 100f;
			RespawnAsteroid ();
		}
		if (!quantedStateObserved && !isMonster){
			quantedStateObserved = true;

			if (quantumProbability < 30f) { // IT WAS A MONSTER ALL A LONG. YOU MONSTER. Quantum physics is a bitch.
				// Transform into a monster!
				isMonster = true;

				hitPoints = Random.Range (100, 125);
				gameObject.GetComponent<Rigidbody2D> ().drag = 1.5f;
			}
		} else{
			hitPoints -= damage; // Die asteroid/monster, die! Reveal to me your bounty.

		}
	}

	void OutOfBounds(GameObject peikko){
		// Kill asteroids that are too far away.
		//if ((-75f >= peikko.transform.position.x) || (75f <= peikko.transform.position.x) || (-75f >= peikko.transform.position.y) || (75f <= peikko.transform.position.y)) {
		float distance = Vector2.Distance(leash.transform.position, peikko.transform.position);
		if (distance > 80){
			Debug.Log ("Asteroid too far away from player!");
			RespawnAsteroid ();
		}
	}

	void DeathFX(){
		if (gameObject.CompareTag ("Green")) {
			Instantiate (Resources.Load ("RockBurst1"), new Vector2 (transform.position.x, transform.position.y), transform.rotation);
		}
		if (gameObject.CompareTag ("Red")) {
			Instantiate (Resources.Load ("RockBurst2"), new Vector2 (transform.position.x, transform.position.y), transform.rotation);
		}
		if (gameObject.CompareTag ("Yellow")) {
			Instantiate (Resources.Load ("RockBurst3"), new Vector2 (transform.position.x, transform.position.y), transform.rotation);
		}
	}
		
	void ThrowRock(GameObject source, GameObject target) {
		GameObject projectile = (GameObject) Instantiate(Resources.Load("RockProjectilePrefab"), source.transform.position, source.transform.rotation); // Create a new asteroid at a randomly rotated vector.
		Vector2 toTarget = target.transform.position - source.transform.position;
		toTarget = toTarget.normalized;
		toTarget = toTarget * 15.0f;
		projectile.GetComponent<Rigidbody2D>().velocity = (toTarget);
	}


	void Update () {
		anim.SetBool (moveHash, isMonster);
		if (isMonster){
			// Find the player and set it as a target if necessary. Replace with something more efficient later.
			if (aggroTarget != null){
				
				// While player is close enough, go after it with the vengeance of a thousand grumpy suns.
				if (Vector2.Distance (transform.position, aggroTarget.transform.position) <= aggroRadius) {

					Vector3 relativePos = aggroTarget.transform.position - gameObject.transform.position;
					float angle = (Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg) + 90;
					Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
					transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);

					if (attackType == 0) { // Those who use ranged attacks keep their distance and flank the player
						intervalCounter += 1;
						throwCooldown += 1;

						if (intervalCounter >= 10) {
							Vector2 viktor = new Vector2 (Random.Range (1f, 2f) * aggroTarget.transform.position.x - gameObject.transform.position.x, Random.Range (1f, 2f) * aggroTarget.transform.position.y - gameObject.transform.position.y);
							gameObject.GetComponent<Rigidbody2D> ().AddForce (viktor * 10f);
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
