﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3( transform.position.x+ Mathf.Repeat(Time.time, 3), transform.position.y+Mathf.Repeat(Time.time, 3), transform.position.z+Mathf.Repeat(Time.time, 3));
	} 
}