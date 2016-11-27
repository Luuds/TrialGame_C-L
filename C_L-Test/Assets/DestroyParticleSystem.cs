using UnityEngine;
using System.Collections;

public class DestroyParticleSystem : MonoBehaviour {
	public float timer;
	// Use th:is for initialization
	void Update () {
		timer +=1f * Time.fixedDeltaTime;
	
		if (timer >= 7f) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Awake () {
		
		//Destroy (gameObject);
	}
}
