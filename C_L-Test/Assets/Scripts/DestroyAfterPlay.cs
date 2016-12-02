using UnityEngine;
using System.Collections;

public class DestroyAfterPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ParticleSystem exp = GetComponent<ParticleSystem> ();
		Destroy (gameObject, exp.duration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
