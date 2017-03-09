using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerSFXEvent : MonoBehaviour {

	public AudioClip meleeAttack;

	public AudioClip footstep;
	private float volLowRange;
	private float volHighRange;
	private float pitchLowRange;
	private float pitchHighRange;

	public AudioSource CurrentSound;

	public void meleeAttackSFXEvent ()
	{
		CurrentSound.pitch = 0.5f;
		CurrentSound.PlayOneShot (meleeAttack, 1);
	}


	public void StepSFXEvent()
	{
		pitchHighRange = 1.5f;
		pitchLowRange = 0.5f;
		volLowRange = 1.5f;
		volHighRange = 2.5f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = randPitch;
		CurrentSound.PlayOneShot (footstep, randVol);
	}
}