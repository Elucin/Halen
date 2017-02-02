using UnityEngine;
using System.Collections;

public class BrawlerSFXManager : MonoBehaviour {
	//use this script to have the brawler make movement sounds and talking sounds.

	//load sound effects
	public AudioClip talkingSFX;

	public AudioClip [] hitSFX;

	public AudioSource CurrentSound;

	//max and min volume for sfx
	private float volLowRange;
	private float volHighRange;

	private float pitchLowRange;
	private float pitchHighRange;



	public void playSoundEffect(string soundID)
	{

		if (soundID == "hit")
		{
			pitchHighRange = 1.2f;
			pitchLowRange = 0.8f;
			volLowRange = 1.0f;
			volHighRange = 1.7f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			int randSound = Random.Range (0,  hitSFX.GetLength(0) - 1);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot(hitSFX[randSound]);
		}

		if (soundID == "talking")
		{
			//pitchHighRange = 1.2f;
			//pitchLowRange = 0.8f;
			//float randVol = Random.Range (volLowRange, volHighRange);
			//float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			//CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot(talkingSFX);
		}
	}
}
