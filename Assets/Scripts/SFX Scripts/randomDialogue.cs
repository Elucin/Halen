using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomDialogue : MonoBehaviour {

	public AudioClip [] dialogue;

	public AudioSource CurrentSound;

	void Awake () {

		int randSound = Random.Range (0, dialogue.GetLength (0) - 1);
		CurrentSound.PlayOneShot (dialogue [randSound], 1);
	}
}
