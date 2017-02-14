using UnityEngine;
using System.Collections;

public class gun_glowing : MonoBehaviour {
	PlayerControl Halen;
	ParticleSystem Glow;
	// Use this for initialization
	void Start () {
		Halen = GameObject.Find("Halen").GetComponent<PlayerControl>();
		Glow = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		if (PlayerControl.isAiming) {
			Glow.Play ();
		} else {
			Glow.Stop ();
		
		}

	}
}
