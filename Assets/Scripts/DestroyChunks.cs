using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChunks : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, Random.Range(3.0f, 10.0f));
	}
}
