using UnityEngine;
using System.Collections;

public class BackgroundBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//gameObject.transform.position (Random.Range (-30f, 30f), Random.Range (-30f, 30f)); // Randomizes the offset of the background for variety.
		//GetComponent<Transform>().position = (Random.Range (-30f, 30f));
		gameObject.transform.position = new Vector2(Random.Range (-30f, 30f), Random.Range (-30f, 30f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
