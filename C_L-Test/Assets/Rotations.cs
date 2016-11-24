using UnityEngine;
using System.Collections;

public class Rotations : MonoBehaviour {

	public Transform Target;
	public Transform TargetL;
	public Transform TargetR;
	public Transform TargetU;
	public Transform TargetD;

	Vector3 target;
	public float speed;
	float tarX; 
	float tarY; 
	float tarXcur; 
	float tarYcur;
	Rigidbody2D rigidbod; 
	// Use this for initialization
	void Start () {
		rigidbod = GetComponent<Rigidbody2D> (); 

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 input = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		Vector3 inputRaw = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		if (input.sqrMagnitude > 1f)
			input.Normalize ();
		if (inputRaw.sqrMagnitude > 1f)
			inputRaw.Normalize ();

		if (inputRaw != Vector3.zero)
			target = input; 

		Vector3 vectorToTarget = target;
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg)- 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
	}
}
