using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargTalking : MonoBehaviour {

	public AudioClip talking;

	public AudioSource CurrentSound;

	//private float volLowRange;
	//private float volHighRange;

	//private float pitchLowRange;
	//private float pitchHighRange;

	private bool isTalking;

	void Start()
	{
		isTalking = false;
	}

	void Update ()
	{
		if (Random.Range (1, 800) == 40 && isTalking == false)
		{
			isTalking = true;
			talk ();
		}

		if (!CurrentSound.isPlaying) 
		{
			isTalking = false;
		}
	}

	public void talk ()
	{
		//CurrentSound.clip.length;
		CurrentSound.PlayOneShot (talking);
	}
}
