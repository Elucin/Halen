using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splineDelay : MonoBehaviour {
	public SplineController spCont;
	public float delay;
	bool running;
	// Use this for initialization
	void Start () {
		running = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (running) {
			delay -= Time.deltaTime;
			if (delay <= 0) {
				spCont.SplineRoot = GameObject.Find ("SplineRoot");
				running = false;

			}
		}
	}
}
