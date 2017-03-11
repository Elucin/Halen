using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableTime : MonoBehaviour {

	public float duration;
	public GameObject[] Object;
	public GameObject[] Activated;
	int amount;
	int amount2;

	// Use this for initialization
	void Start () {
		amount = Object.Length;
		amount2 = Activated.Length;
		if (duration == null) {
			for (int i = 0; i < amount; i++) {
				Object[i].SetActive (false); 
			}
			for (int j = 0; j < amount2; j++) {
				Activated[j].SetActive (true); 
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
			for (int j = 0; j < amount2; j++) {
				Activated[j].SetActive (true); 
			}
			gameObject.SetActive (false); 
		}
	}
}
