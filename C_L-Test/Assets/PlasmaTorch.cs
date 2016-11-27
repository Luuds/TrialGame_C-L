using UnityEngine;
using System.Collections;

public class PlasmaTorch : MonoBehaviour {
	ParticleSystem Plasma;
	float colorF;
	public static float probabilityInterpol;

	// Use this for initialization
	void Start () {
		Plasma = GetComponent<ParticleSystem> ();

		Plasma.Stop() ;
	}
		
	
	// Update is called once per frame
	void Update(){
		
		colorF += 0.6f * Input.GetAxis ("Mouse ScrollWheel");
		if (colorF <=0){
			colorF = 0;
		}
		if (colorF >=0.8f){
			colorF = 0.8f;
		}
		Color plasmaColor = new Color ( 0.8f -( colorF/2), colorF + 0.2f, 0.2f);
		Plasma.startColor = plasmaColor;
		//Debug.Log (colorF.ToString());
		if (Input.GetMouseButtonDown (0)) {
			Plasma.Play ();


		}
		if (Input.GetMouseButtonUp(0)){
			Plasma.Stop() ;

		}

		probabilityInterpol = ((colorF * 100f) / 0.8f);

	}
}
