using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneRandom : MonoBehaviour {

    
	public Material[] RuneImage;
	int Selector;

	// Use this for initialization
	void Start () {
		Selector = Random.Range (0, 20);
		gameObject.GetComponent<MeshRenderer> ().material = RuneImage [Selector];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
