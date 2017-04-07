using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPanelDialogueTrig : MonoBehaviour {

	public AudioClip Dialogue;

	public AudioSource CurrentSound;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Enemy")
		{
			CurrentSound.PlayOneShot (Dialogue, 1);
		}
	}
}
