using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableTime : MonoBehaviour {

	public float duration;
	public GameObject[] Object;
	int amount;

	// Use this for initialization
	void Start () {
		amount = Object.Length;
		if (duration == null) {
			for (int i = 0; i < amount; i++) {
				Object[i].SetActive (false); 
			}
			gameObject.SetActive (false); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		duration -= Time.deltaTime;
		if (duration <= 0) {
			for (int i = 0; i < amount; i++) {
				Object[i].SetActive (false); 
			}
			gameObject.SetActive (false); 
		}
	}
}
