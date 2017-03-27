using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraChromaticAberration : MonoBehaviour {




	// Use this for initialization
	void Start () {
		this.GetComponent<VignetteAndChromaticAberration> ().intensity = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerControl.Health < 53) {
			this.GetComponent<VignetteAndChromaticAberration> ().chromaticAberration = (100-PlayerControl.Health )/4;

		} else {
			this.GetComponent<VignetteAndChromaticAberration> ().chromaticAberration = 0;

		}

	}
}
