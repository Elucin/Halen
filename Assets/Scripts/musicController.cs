using UnityEngine;
using System.Collections;

public class musicController : MonoBehaviour {
	
	public UnityEngine.Audio.AudioMixer mixer;

	public void ChangeMusic(UnityEngine.Audio.AudioMixerSnapshot music)
	{
		music.TransitionTo (5);
	}
}
