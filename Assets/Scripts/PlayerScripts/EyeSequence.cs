using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSequence : MonoBehaviour {

	public halenEyes_Script eyeScript;
	public halenEyes_Script.EyeStruct[] EyeStep;

	public bool triggered = false; 

	// Use this for initialization
	void Start () {
		if (eyeScript == null) {
			eyeScript = GameObject.FindObjectOfType<halenEyes_Script> ();
		}
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider c) {
		if (c.CompareTag ("Player")) {
			eyeScript.RunSequence (EyeStep);
			Destroy (gameObject);
		}
	}
	void Update() {
		if(triggered)
		{
			eyeScript.RunSequence (EyeStep);
			Destroy (gameObject);
		}
	
	}






}
