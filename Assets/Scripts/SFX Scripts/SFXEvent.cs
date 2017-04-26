using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXEvent : MonoBehaviour {

	public AudioClip rollSFX;

	public AudioClip [] meleeSFX;

	public AudioClip [] stoneStepSFX;

	public AudioSource CurrentSound;

	private float volLowRange;
	private float volHighRange;

	private float pitchLowRange;
	private float pitchHighRange;

    void Start()
    {
        CurrentSound = GameObject.Find("SoundManager").GetComponent<AudioSource>();
    }

	public void RollSFXEvent()
	{
		pitchHighRange = 1.5f;
		pitchLowRange = 1.0f;
		volLowRange = 0.5f;
		volHighRange = 0.2f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = randPitch;

		CurrentSound.PlayOneShot (rollSFX, randVol);
	}

	public void MeleeSFXEvent()
	{
		Debug.Log ("SHWING");
		pitchHighRange = 1.2f;
		pitchLowRange = 0.8f;
		volLowRange = 0.5f;
		volHighRange = 0.8f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = randPitch;
		int randSound = Random.Range (0, meleeSFX.GetLength (0) - 1);
		CurrentSound.PlayOneShot (meleeSFX [randSound], randVol);
	}


	public void StepSFXEvent()
	{
		pitchHighRange = 1.2f;
		pitchLowRange = 0.8f;
		volLowRange = 0.05f;
		volHighRange = 0.2f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = randPitch;
		int randSound = Random.Range (0, stoneStepSFX.GetLength (0) - 1);
		CurrentSound.PlayOneShot (stoneStepSFX [randSound], randVol - 0.02f);
	}
}