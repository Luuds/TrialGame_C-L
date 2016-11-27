using UnityEngine;
using System.Collections;

public class BackgroundBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector2(Random.Range (-5f, 5f), Random.Range (-5f, 5f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
