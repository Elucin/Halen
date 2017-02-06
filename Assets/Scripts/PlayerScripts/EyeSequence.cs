using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSequence : MonoBehaviour {

	halenEyes_Script eyeScript;
	public halenEyes_Script.EyeStruct[] EyeStep;

	// Use this for initialization
	void Start () {
		eyeScript = GameObject.FindObjectOfType<halenEyes_Script> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider c) {
		if (c.CompareTag ("Player")) {
			eyeScript.RunSequence (EyeStep);
			Destroy (gameObject);
		}
	}


}
