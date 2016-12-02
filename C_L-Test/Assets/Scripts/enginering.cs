using UnityEngine;
using System.Collections;

public class enginering : MonoBehaviour {

	public float rotationSpeed = 3.6f;
	public float currentRotation = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		GetComponent<Rigidbody2D> ().rotation += (Input.GetAxis ("Horizontal") * rotationSpeed);

		/*if (Input.GetKey (KeyCode.LeftArrow))
			GetComponent<Rigidbody2D> ().rotation += rotationSpeed;
		
		if (Input.GetKey (KeyCode.RightArrow))
			GetComponent<Rigidbody2D> ().rotation -= rotationSpeed;

		*/
			
	
	}
}
