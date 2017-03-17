using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkVolume : MonoBehaviour {

	public AudioSource Voice;
	float randomDuration;
	int randomizer;

	// Use this for initialization
	void Start () {
		Voice.volume = 0f;
		randomDuration = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (randomDuration <= 0) {
			Voice.volume = 0f;
			randomizer = Random.Range (0, 4);
			if (randomizer == 0) {
				Voice.volume = 1f;
			}
			randomDuration = Random.Range (1, 6);
		} else {
			randomDuration -= Time.deltaTime;
		}
	}
}
