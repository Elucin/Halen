using UnityEngine;
using System.Collections;

public class PlayerSFXManager : MonoBehaviour 
{
	//load sound effects
	public AudioClip smallShotSFX;
	public AudioClip largeShotSFX;
	public AudioClip dashSFX;
	public AudioClip dashReadySFX;

	public AudioClip jump1SFX;
	public AudioClip jump2SFX;


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
			volLowRange = 0.4f;
			volHighRange = 0.8f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (smallShotSFX, randVol);
		}
		else if (soundID == "largeShot") {
			pitchHighRange = 1.2f;
			pitchLowRange = 0.8f;
			volLowRange = 1.5f;
			volHighRange = 2.0f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (largeShotSFX, randVol);
		}
		else if (soundID == "dash") {
			pitchHighRange = 1.0f;
			pitchLowRange = 1.0f;
			volLowRange = 0.5f;
			volHighRange = 1.0f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (dashSFX, randVol);
		}
		else if (soundID == "dashReady") {
			CurrentSound.pitch = 1;
			CurrentSound.PlayOneShot (dashReadySFX, 1.0f);
		}
		else if (soundID == "jump1") {
			pitchHighRange = 1.8f;
			pitchLowRange = 2.0f;
			volLowRange = 0.3f;
			volHighRange = 0.7f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (jump1SFX, randVol);
		}
		else if (soundID == "jump2") {
			pitchHighRange = 0.8f;
			pitchLowRange = 1.2f;
			volLowRange = 0.3f;
			volHighRange = 0.7f;
			float randVol = Random.Range (volLowRange, volHighRange);
			float randPitch = Random.Range (pitchLowRange, pitchHighRange);
			CurrentSound.pitch = randPitch;
			CurrentSound.PlayOneShot (jump2SFX, randVol);
		}
	}
}