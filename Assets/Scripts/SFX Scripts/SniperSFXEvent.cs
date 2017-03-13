using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperSFXEvent : MonoBehaviour {

	public AudioClip shot;

	public AudioClip sTransform;

	public AudioClip footstep;
	private float volLowRange;
	private float volHighRange;
	private float pitchLowRange;
	private float pitchHighRange;


	public AudioSource CurrentSound;


	public void shotSFXEvent ()
	{
		CurrentSound.PlayOneShot (shot, 1);
	}

	public void transformSFXEvent ()
	{
		CurrentSound.PlayOneShot (sTransform, 2);
	}

	public void StepSFXEvent()
	{
		pitchHighRange = 1.5f;
		pitchLowRange = 0.5f;
		volLowRange = 0.5f;
		volHighRange = 1.5f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = randPitch;
		CurrentSound.PlayOneShot (footstep, randVol);

	}
}
