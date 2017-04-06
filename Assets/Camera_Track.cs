using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Track : MonoBehaviour {

	public float xSpeed;
	public float ySpeed;
	public float zSpeed;
	public float Duration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Duration > 0) {
			Duration -= Time.deltaTime;
			this.transform.localPosition+= new Vector3(xSpeed*Time.deltaTime, ySpeed*Time.deltaTime, zSpeed*Time.deltaTime);

		}
	}
}
