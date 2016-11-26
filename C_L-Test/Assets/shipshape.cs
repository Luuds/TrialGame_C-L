using UnityEngine;
using System.Collections;

public class shipshape : MonoBehaviour {

	public GameObject peikko;
	public GameObject player;
	public float maxSpeed = 10f;
	public float accelerationRate = 5f;
	public float rotationSpeed = 3.6f;
	public float speed;

	public Quaternion engineRotation;
	Rigidbody2D rb;
	Rigidbody2D peikkos;
	Rigidbody2D playerrb;
	Vector3 target; 
	Vector2 mousepos; 
	Vector3 screenpos; 
	Vector3 zEU; 
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		peikkos = peikko.GetComponent<Rigidbody2D> ();
		target = new Vector2 (0, 1); 
		playerrb = player.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame


	void FixedUpdate () {

		Vector3 input = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		Vector3 inputRaw = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (input.sqrMagnitude > 1f) {
			input.Normalize ();
		}
		if (inputRaw.sqrMagnitude > 1f) {
			inputRaw.Normalize ();
		}
		if (inputRaw != Vector3.zero) {
			target = input; 
		}
		Vector3 vectorToTarget = target;
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg)- 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		peikkos.transform.rotation = Quaternion.Slerp(peikkos.transform.rotation, q, Time.deltaTime * speed);

		//rb.rotation += (Input.GetAxis ("Cabin") * rotationSpeed);
		//engineRotation = peikko.transform.rotation;
		Vector2 viktor = peikkos.transform.rotation * Vector2.up ;

		if(Input.GetAxisRaw("Vertical")!= 0||Input.GetAxisRaw("Horizontal")!=0){
			//rb.AddForce (viktor * accelerationRate);
			//peikkos.AddForce (viktor *accelerationRate);
			playerrb.AddForce (viktor *accelerationRate);
		}
		mousepos = Input.mousePosition;
		screenpos = Camera.main.ScreenToWorldPoint (new Vector3 (mousepos.x, mousepos.y, transform.position.z - Camera.main.transform.position.z)); 
		zEU = transform.eulerAngles;
		zEU.z = (Mathf.Atan2 ((screenpos.y - transform.position.y), (screenpos.x - transform.position.x)) * Mathf.Rad2Deg) - 100; 
		transform.eulerAngles = zEU; 
	}
}
