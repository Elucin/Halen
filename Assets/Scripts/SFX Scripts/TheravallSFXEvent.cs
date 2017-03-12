using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheravallSFXEvent : MonoBehaviour {


	public AudioClip shoot;
	public AudioClip teleportOut;
	public AudioClip teleportIn;
	public AudioClip footstep;
	public AudioClip melee;
	public AudioClip taunt;
	public AudioClip aim;
	public AudioClip fire;


	private float volLowRange;
	private float volHighRange;
	private float pitchLowRange;
	private float pitchHighRange;

	public AudioSource CurrentSound;
	public AudioSource CurrentVO;

	public void shootSFXEvent()
	{
		pitchHighRange = 1.5f;
		pitchLowRange = 0.5f;
		volLowRange = 0.8f;
		volHighRange = 1.2f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = randPitch;
		CurrentSound.PlayOneShot (shoot, randVol);
	}

	public void teleportOutSFXEvent ()
	{
		//CurrentSound.PlayOneShot (teleportOut, 1);
	}

	public void teleportInSFXEvent ()
	{
		//CurrentSound.PlayOneShot (teleportIn, 1);
	}

	public void meleeSFXEvent ()
	{
		CurrentSound.PlayOneShot (melee, 1);
	}
		
	public void railGunAimSFXEvent ()
	{
		CurrentSound.PlayOneShot (aim, 1);
	}

	public void railGunShootSFXEvent ()
	{
		CurrentSound.PlayOneShot (fire, 1);
	}

	public void StepSFXEvent()
	{
		pitchHighRange = 1.5f;
		pitchLowRange = 0.5f;
		volLowRange = 0.8f;
		volHighRange = 1.2f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = randPitch;
		CurrentSound.PlayOneShot (footstep, randVol);
	}
}
