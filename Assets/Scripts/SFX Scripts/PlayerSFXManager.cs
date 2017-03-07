using UnityEngine;
using System.Collections;

public class PlayerSFXManager : MonoBehaviour 
{
	//load sound effects
	public AudioClip smallShotSFX;
	public AudioClip largeShotSFX;
	public AudioClip dashSFX;
	public AudioClip dashReadySFX;

	public AudioClip [] footStepSFX;

	public AudioSource CurrentSound;

	//max and min volume for sfx
	private float volLowRange;
	private float volHighRange;

	private float pitchLowRange;
	private float pitchHighRange;

	Animator anim;

	void Start()
	{
		anim = GameObject.Find("Halen").GetComponent<Animator> ();

	}

	public void playSoundEffect(string soundID)
	{
		
		if (soundID == "smallShot") {
			pitchHighRange = 1.0f;
			pitchLowRange = 0.8f;
			volLowRange = 0.5f;
			volHighRange = 1.0f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (smallShotSFX, randVol);
		} else if (soundID == "largeShot") {
			pitchHighRange = 1.2f;
			pitchLowRange = 0.8f;
			volLowRange = 0.5f;
			volHighRange = 1.0f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (largeShotSFX, randVol);
		} else if (soundID == "Footstep") {
			pitchHighRange = 1.2f;
			pitchLowRange = 0.8f;
			volLowRange = 1.5f;
			volHighRange = 2.0f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			int randSound = Random.Range (0, footStepSFX.GetLength (0) - 1);
			CurrentSound.PlayOneShot (footStepSFX [randSound], randVol);
		} else if (soundID == "dash") {
			pitchHighRange = 1.2f;
			pitchLowRange = 0.8f;
			volLowRange = 0.5f;
			volHighRange = 1.0f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (dashSFX, randVol);
		} else if (soundID == "jump") {
			pitchHighRange = 2.0f;
			pitchLowRange = 1.7f;
			volLowRange = 0.5f;
			volHighRange = 1.0f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (dashSFX, randVol - 0.2f);

		} else if (soundID == "dashReady") {
			CurrentSound.pitch = 1;
			CurrentSound.PlayOneShot (dashReadySFX, 0.5f);
		}
	}
}