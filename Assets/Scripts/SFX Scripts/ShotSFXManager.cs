using UnityEngine;
using System.Collections;

public class ShotSFXManager : MonoBehaviour {


	//load sound effects
	public AudioClip hitSFX;

	public AudioSource CurrentSound;

	//max and min volume for sfx
	private float volLowRange = 0.5f;
	private float volHighRange = 1.0f;

	private float pitchLowRange;
	private float pitchHighRange;

	public void playSoundEffect(string soundID)
	{

		if (soundID == "hit")
		{
			pitchHighRange = 1.2f;
			pitchLowRange = 0.8f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot(hitSFX);
			Debug.Log ("Sup");
		}
	}
}
