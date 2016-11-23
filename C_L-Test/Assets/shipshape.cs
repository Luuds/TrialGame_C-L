using UnityEngine;
using System.Collections;

public class shipshape : MonoBehaviour {

	public GameObject peikko;

	public float maxSpeed = 10f;
	public float accelerationRate = 5f;
	public float rotationSpeed = 3.6f;

	public float currentSpeed = 0f;
	public Quaternion engineRotation;
	Rigidbody2D rb;
	Rigidbody2D peikkos;
	Transform ship;
	Transform target; 
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		peikkos = peikko.GetComponent<Rigidbody2D> ();
		ship = transform; 
	}

	// Update is called once per frame


	void FixedUpdate () {
		target.position.z = (Input.GetAxis ("Horizontal") * 90);
		Vector2 relativepos = target.position - transform.position; 
		Quaternion rotation = Quaternion.LookRotation (relativepos);

		peikkos.rotation += (Input.GetAxis ("Horizontal") * rotationSpeed );
		rb.rotation += (Input.GetAxis ("Cabin") * rotationSpeed);
		engineRotation = peikko.transform.rotation;
		Vector2 viktor = engineRotation * Vector2.up;
		rb.AddForce (viktor * Input.GetAxis("Vertical"));
		peikkos.AddForce (viktor * Input.GetAxis("Vertical"));

		//transform.rotation = ship.rotation;




	
	}
}
