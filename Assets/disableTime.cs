using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableTime : MonoBehaviour {

	public float duration;

	// Use this for initialization
	void Start () {
		if (duration == null) {
			gameObject.SetActive (false); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		duration -= Time.deltaTime;
		if (duration <= 0) {
			gameObject.SetActive (false); 
		}
	}
}
