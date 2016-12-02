using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIResources : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		float resources = PlayerBehavior.resourcesGathered;
		resources = Mathf.Round (resources);
		text.text = "Resources: " + resources;
	}
}
