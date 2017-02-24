using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSequence : MonoBehaviour {
	[System.Serializable]
	public struct CameraStruct
	{
		public GameObject Camera;
		public float duration;
		public GameObject Subject;
		public EyeSequence Eyes;

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
			if (index != 0) {
				CamStep [index-1].Camera.SetActive (false);
				if (CamStep [index - 1].Subject != CamStep [index].Subject) {
					CamStep [index - 1].Subject.SetActive (false);
				}
			}
			CamStep [index].Camera.SetActive (true);
			CamStep[index].Subject.SetActive (true);
			if (CamStep [index].Eyes != null) {
				CamStep [index].Eyes.triggered = true;
			}
		

			goTime = false;
			Timer = 0;

		}
		else {
			if (Timer >= CamStep [index].duration) {
				if (index + 1 < CamStep.Length) {
					index++;
					goTime = true;
				}
			} else {
				Timer += Time.deltaTime; 
			}
		}

	}
}
