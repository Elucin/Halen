using UnityEngine;
using System.Collections;

public class musicTrigger : MonoBehaviour {

	//Load in all music
	public UnityEngine.Audio.AudioMixerSnapshot mainTheme;
	public UnityEngine.Audio.AudioMixerSnapshot combatTheme;
	public UnityEngine.Audio.AudioMixerSnapshot ambientTheme;
	public UnityEngine.Audio.AudioMixerSnapshot discoveryTheme;

	//Instantiating music controller script
	private musicController _musicController;

	// Use this for initialization
	void Start () 
	{
		_musicController = FindObjectOfType<musicController> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && gameObject.tag == "MusicZone" && gameObject.name == "CombatZone") 
		{
			if(combatTheme != null)
			{
				_musicController.ChangeMusic (combatTheme);
			}
		}

		if (other.tag == "Player" && gameObject.tag == "MusicZone" && gameObject.name == "MainThemeZone") 
		{
			if(mainTheme != null)
			{
				_musicController.ChangeMusic (mainTheme);
			}
		}

		if (other.tag == "Player" && gameObject.tag == "MusicZone" && gameObject.name == "DiscoveryZone") 
		{
			if(discoveryTheme != null)
			{
				_musicController.ChangeMusic (discoveryTheme);
			}
		}

		if (other.tag == "Player" && gameObject.tag == "MusicZone" && gameObject.name == "AmbientThemeZone") 
		{
			if(ambientTheme != null)
			{
				_musicController.ChangeMusic (ambientTheme);
			}
		}

	}		


}