using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXEvent : MonoBehaviour {

	public AudioClip rollSFX;

	public AudioSource CurrentSound;

	private float volLowRange;
	private float volHighRange;

	private float pitchLowRange;
	private float pitchHighRange;

	public void RollSFXEvent()
	{
		volLowRange = 0.5f;
		volHighRange = 1.0f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = 1;



		CurrentSound.PlayOneShot (rollSFX, randVol - 0.2f);

	}
}
