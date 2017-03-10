using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerSFXEvent : MonoBehaviour {

	public AudioClip scream;

	public AudioClip fistPound;

	public AudioClip groundPound;

	public AudioClip slide;

	public AudioClip footstep;
	private float volLowRange;
	private float volHighRange;
	private float pitchLowRange;
	private float pitchHighRange;


	public AudioSource CurrentSound;


	public void screamSFXEvent ()
	{
		CurrentSound.PlayOneShot (scream, 1);
	}

	public void fistPoundSFXEvent ()
	{
		CurrentSound.PlayOneShot (fistPound, 1);
	}

	public void groundPoundSFXEvent ()
	{
		CurrentSound.PlayOneShot (groundPound, 1);
	}

	public void slideSFXEvent ()
	{
		CurrentSound.pitch = 0.9f;
		CurrentSound.PlayOneShot (slide, 2);
	}

	public void StepSFXEvent()
	{
		pitchHighRange = 1.5f;
		pitchLowRange = 1.0f;
		volLowRange = 0.5f;
		volHighRange = 1.0f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = randPitch;
		CurrentSound.PlayOneShot (footstep, randVol);
	}
}
