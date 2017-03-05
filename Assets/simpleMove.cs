using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleMove : MonoBehaviour {
	public Transform startPoint;
	public Transform endPoint;
	public float duration;
	float progress;
	float counter;



	// Use this for initialization
	void Start () {
		counter = 0;

	}
	
	// Update is called once per frame
	void Update () {

		progress = counter/duration;

		this.transform.position = Vector3.Lerp (startPoint.position, endPoint.position, progress);
		this.transform.rotation = Quaternion.Lerp (startPoint.rotation, endPoint.rotation, progress);
		counter +=Time.deltaTime;
		if (counter > duration) {
			Destroy (this);
		}
	
	}


}
