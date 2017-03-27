using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSFX : MonoBehaviour {

	public AudioClip [] explosionSFX;

	public AudioSource CurrentSound;

	private float volLowRange;
	private float volHighRange;
	private float pitchLowRange;
	private float pitchHighRange;

	// Use this for initialization
	void Awake () {

		pitchHighRange = 1.5f;
		pitchLowRange = 0.5f;
		volLowRange = 0.5f;
		volHighRange = 1.5f;
		float randVol = Random.Range (volLowRange, volHighRange);
		float randPitch = Random.Range (pitchLowRange, pitchHighRange);
		CurrentSound.pitch = randPitch;

		int randSound = Random.Range (0, explosionSFX.GetLength (0) - 1);
		CurrentSound.PlayOneShot (explosionSFX [randSound], randVol);
	}
}
