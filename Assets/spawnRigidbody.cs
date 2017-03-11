using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRigidbody : MonoBehaviour {

	public float counter;
	public GameObject Subject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (counter <= 0) {
			gameObject.AddComponent<Rigidbody> ();
			Destroy (this);
		}
		counter -= Time.deltaTime;
	}
}
