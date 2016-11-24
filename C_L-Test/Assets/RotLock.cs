using UnityEngine;
using System.Collections;

public class RotLock : MonoBehaviour {

	public GameObject ship; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = ship.transform.position + transform.position;
	}
}
