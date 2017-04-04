using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {

	public ParticleSystem CollectedParticle;
	public Transform CollectTrans;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider c)
	{
		if (c.CompareTag("Player")) {
			Collected ();
		}
	}

	void Collected()
	{
		//[Add points]
		Scoring.AddScore(this.transform,0,100,0);
		ParticleSystem CollectParticle = Instantiate (CollectedParticle, CollectTrans.position, Quaternion.identity) as ParticleSystem;

		Destroy (gameObject);

	}
}
