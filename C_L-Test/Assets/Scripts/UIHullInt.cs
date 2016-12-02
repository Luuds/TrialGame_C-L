using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHullInt : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	
	}

	// Update is called once per frame
	void Update () {
		float hitPoints = PlayerBehavior.hitPoints;
		hitPoints = Mathf.Round (hitPoints);
		text.text = "Hull integrity: " + hitPoints;
		if (hitPoints < 25f){
			text.fontSize = 32;
		} else
			text.fontSize = 24;
	}
}
