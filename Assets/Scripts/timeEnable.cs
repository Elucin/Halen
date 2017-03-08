using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeEnable : MonoBehaviour {

	public float duration;
	public GameObject[] Object;
	int amount;
	public GameObject[] Hidden;
	int amount2;

	// Use this for initialization
	void Start () {
		
		amount = Object.Length;
		amount2 = Hidden.Length;
		if (duration == null) {
			for (int i = 0; i < amount; i++) {
				Object[i].SetActive (true); 
			}
			for (int j = 0; j < amount2; j++) {
				Hidden[j].SetActive (false); 
			}

			Destroy (this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		duration -= Time.deltaTime;
		if (duration <= 0) {
			for (int i = 0; i < amount; i++) {
				Object[i].SetActive (true); 
			}
			for (int j = 0; j < amount2; j++) {
				Hidden[j].SetActive (false); 
			}
			Destroy (this);
		}
	}
}
