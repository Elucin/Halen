﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSequence : MonoBehaviour {
	[System.Serializable]
	public struct CameraStruct
	{
		public GameObject Camera;
		public float duration;
		public GameObject Subject;
	};

	public CameraStruct[] CamStep;

	int index = 0;
	bool goTime;

	float Timer;
	// Use this for initialization
	void Start () {
		goTime = true;
	}

	
	// Update is called once per frame
	void Update () {
		if (goTime) {
			
			CamStep [index].Camera.SetActive (true);
			CamStep[index].Subject.SetActive (true);
			if (index != 0) {
				CamStep [index-1].Camera.SetActive (false);
				CamStep [index-1].Subject.SetActive (false);
			}
			goTime = false;
			Timer = 0;

		}
		else {
			if (Timer >= CamStep [index].duration) {
				index++;
				goTime = true;
			} else {
				Timer += Time.deltaTime; 
			}
		}

	}
}
