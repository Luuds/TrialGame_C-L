using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {


	RaycastHit2D targetedBody;
	public Collider2D col;
	bool locked = false;
	public float targetLockRadius = 1f;
	public LayerMask targetFilter;
	public GameObject launchPoint;
	public GameObject grandpa;


	// Use this for initialization
	void Start () {

	}


	// Create the missile and set starting vector and speed. After that it's up to the missile's own guidance systems.
	void LaunchMissile(Collider2D target) {
		GameObject projectile = (GameObject)Instantiate (Resources.Load ("Missile"), launchPoint.transform.position, launchPoint.transform.rotation);

		// Inherit target and velocity (not angular velocity, then shit gets weird)
		projectile.GetComponent<Missile> ().target = target;
		projectile.GetComponent<Rigidbody2D> ().velocity = grandpa.GetComponent<Rigidbody2D> ().velocity;
		//projectile.GetComponent<Rigidbody2D>().angularVelocity = grandpa.GetComponent<Rigidbody2D>().angularVelocity;

		// Add a bit of initial force
		if (target != null) {
			Vector3 relativePos = target.gameObject.transform.position - projectile.transform.position;
			relativePos = relativePos.normalized * 20f;
			projectile.GetComponent<Rigidbody2D> ().AddForce (relativePos);
		}



	}
		
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && PlayerBehavior.resourcesGathered > 10f) {
			if (locked){
				PlayerBehavior.resourcesGathered -= 10f; // Costs 10 resources to fire a missile
				LaunchMissile (col); // Launches the missile if mouse is pressed when on a valid target
			} else
				Debug.Log ("No target lock or not enough resources.");
		}

		Vector2 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		targetedBody = Physics2D.CircleCast (mouseInWorld, targetLockRadius, mouseInWorld, targetLockRadius, targetFilter);
		col = targetedBody.collider;
		locked = (col != null);

	}
}
