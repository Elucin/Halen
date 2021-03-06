﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class halenEyes_Script : MonoBehaviour {

	Animator EyeLookanim;
	public static int eyeY; 
	public static int eyeX;
	public static bool finished = true;
    public static int currentEyeIndex = 13;
	public Texture2D[] Eyes = new Texture2D[14];
	bool blink = true;
	bool running = false;
	[System.Serializable]
	public struct EyeStruct
	{
		public int index;
		public float duration;
		public float eyeDirectionY;
		public float eyeDirectionX;
	};

	/*
	 * eye direction
	 * eyeX - -1 is left, 1 is right
	 * eyeY - 1 is up, -1 is down
	 * 
	 * 
	 * eye expression
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
		EyeLookanim = transform.parent.GetComponent<Animator> ();
		eyeY = Animator.StringToHash ("EyesY");
		eyeX = Animator.StringToHash ("EyesX");

	}
	
	// Update is called once per frame
	void Update () {
		if (blink && !running) {
			float delay = Random.Range (3f, 8f);
			StartCoroutine (Blink (delay));
		}
		//StartCoroutine (EyeExpression (0, 2,true));

	}

	public IEnumerator EyeExpression(int EyeIndex, float Duration, bool ReturntoNeutral = true, float EyeY =0f, float EyeX =0f,  EyeStruct[] e = null, int i = 0)
	{
		GetComponent<MeshRenderer> ().material.mainTexture = Eyes [EyeIndex];
        currentEyeIndex = EyeIndex;
		EyeLookanim.SetFloat (eyeX, EyeX);
		EyeLookanim.SetFloat (eyeY, EyeY);
		yield return new WaitForSeconds (Duration);
		if (ReturntoNeutral ==true) {
			GetComponent<MeshRenderer> ().material.mainTexture = Eyes [13];
			EyeLookanim.SetFloat (eyeX, 0f);
			EyeLookanim.SetFloat (eyeY, 0f);
		}
		if(e != null)
			RunSequence (e, ++i); 
		
		
	}

	public void RunSequence(EyeStruct[] e, int i = 0)
	{
		//if (halenEyes_Script.finished) {
		if (i < e.Length) {
			running = true;
			StartCoroutine (EyeExpression (e [i].index, e [i].duration, i == e.Length - 1, e[i].eyeDirectionY,e[i].eyeDirectionX,  e, i));
		}
		else
			running = false;
		//}
	}

	IEnumerator Blink(float delay)
	{
		blink = false;
		yield return new WaitForSeconds (delay);
        StartCoroutine(EyeExpression (0, 0.1f, true, -0.5f,0f));
		blink = true;
	}
}
