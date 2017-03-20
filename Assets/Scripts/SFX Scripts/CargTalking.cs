using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargTalking : MonoBehaviour {

	public AudioClip talking;
	AIBase ai;
	public AudioSource CurrentSound;

	int randomizer;
	float randomDuration;

	//private float volLowRange;
	//private float volHighRange;

	//private float pitchLowRange;
	//private float pitchHighRange;

	private bool isTalking;

	void Start()
	{
		//CurrentSound = GetComponent<AudioSource> ();
		ai = GetComponent<AIBase> ();
	}

	void Update ()
	{

		if (randomDuration <= 0) {
			CurrentSound.volume = 0f;
			randomizer = Random.Range (0, 4);
			if (randomizer == 0) {
				Debug.Log ("Talking");
				CurrentSound.volume = 1f;
			}
			randomDuration = (Random.value*3.5f)+0.5f;
		} else {
			randomDuration -= Time.deltaTime;
		}
		/*
		if (Random.Range (1, 800) == 40 && isTalking == false)
		{
			isTalking = true;
			talk ();
		}
		if (!CurrentSound.isPlaying) 
		{
			isTalking = false;
		}
		*/
	}

	public void talk ()
	{
		//CurrentSound.clip.length;

	}
}
