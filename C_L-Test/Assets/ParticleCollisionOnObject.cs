﻿using UnityEngine;
using System.Collections;

public class ParticleCollisionOnObject : MonoBehaviour {


	SpriteRenderer spr; 
	Color newColour; 
	int particleCollisionCount; 
	GameObject TemObj;
	// Use this for initialization
	void Start () {

	}
	void OnParticleCollision (GameObject other){
		
			particleCollisionCount += 1; 
			
	}
	// Update is called once per frame
	void Update () {
		if (particleCollisionCount >= 100) {

			particleCollisionCount = 0;
			Destroy (gameObject); 
		}
		Debug.Log (particleCollisionCount.ToString());
	}
}
