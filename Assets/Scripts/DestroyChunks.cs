using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChunks : MonoBehaviour {
	public float minTime;
	public float maxTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, Random.Range(minTime, maxTime));
	}
}
