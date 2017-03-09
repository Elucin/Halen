using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlerSFXEvent : MonoBehaviour {

	public AudioClip lightAttack;

	public AudioClip heavyAttack;

	public AudioClip footstep;
	private float volLowRange;
	private float volHighRange;
	private float pitchLowRange;
	private float pitchHighRange;


	public AudioSource CurrentSound;


	public void lightAttackSFXEvent ()
	{
		CurrentSound.PlayOneShot (lightAttack, 1);
	}

	public void heavyAttackSFXEvent ()
	{
		CurrentSound.PlayOneShot (heavyAttack, 2);
	}

	public void StepSFXEvent()
	{
		if (CurrentSound.clip != heavyAttack) {
			pitchHighRange = 1.5f;
			pitchLowRange = 0.5f;
			volLowRange = 0.5f;
			volHighRange = 1.5f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (footstep, randVol);
		}
		else if (CurrentSound.clip == heavyAttack){
			Debug.Log ("Attack");
		}
	}
}
