using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public static float hitPoints = 100f;
	public static float resourcesGathered = 0f;
	float recentlyDamaged = 5f; //
	float damageTimer = 0f;

	// Use this for initialization
	void Start () {
		hitPoints = 100f;
		resourcesGathered = 0f;
	}


	// This function needs to be where the collider is, that's why it's not in "shipshape"
	void ApplyDamage(float damage){
		hitPoints -= damage;
		damageTimer = 0; // Reset timer before ship starts repairing itself
		if (hitPoints <= 0)
			SceneManager.LoadScene("ChrickeTest");
	}
	
	// Update is called once per frame
	void Update () {
		damageTimer += 1 * Time.deltaTime;
		if (damageTimer >= recentlyDamaged && hitPoints < 100) {
			hitPoints += 0.1f; // Repair ship if it hasn't taken damage in the last few seconds
			if (hitPoints > 100)
				hitPoints = 100;
		}
	}
}
