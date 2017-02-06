using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class halenEyes_Script : MonoBehaviour {



	public Texture2D[] Eyes = new Texture2D[14];
	/*
	0 - squint
	1 - confused
	2 - annoyed
	3 - unimpressed 2
	4 - unimpressed
	5 - unimpressed "tapper"
	6 - happy
	7 - sad
	8 - anger 2
	9 - anger
	10 - surprised 2
	11 - surprised 1
	12 - surprised 
	13 - neutral
	*/




	// Use this for initialization
	void Start () {
		//This is what it looks like to change the expression. Just specify what expression needed
		//GetComponent<MeshRenderer> ().material.mainTexture = Eyes [0];
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
