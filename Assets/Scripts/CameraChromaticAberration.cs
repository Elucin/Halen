using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraChromaticAberration : MonoBehaviour {




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<VignetteAndChromaticAberration> ().chromaticAberration = (100-PlayerControl.Health )/4;
	}
}
