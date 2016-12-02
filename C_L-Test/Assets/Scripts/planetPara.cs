using UnityEngine;
using System.Collections;

public class planetPara : MonoBehaviour {
	Vector3 offset;
	Vector3 velocity = Vector3.zero;
	public float smoothtime = 0.01f;
	// Use this for initialization
	void Start () {
		offset = transform.position + GameObject.FindWithTag ("Player").transform.position;
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.SmoothDamp (transform.position, GameObject.FindWithTag ("Player").transform.position + offset / 10,ref velocity ,smoothtime);
	}
}


