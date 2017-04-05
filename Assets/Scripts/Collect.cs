using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {

	public ParticleSystem CollectedParticle;
	public Transform CollectTrans;

	void OnTriggerEnter (Collider c)
	{
		if (c.CompareTag("Player")) {
			Collected ();
		}
	}

	void Collected()
	{
		//[Add points]
		Scoring.AddScore(this.transform,0,1000,0);
		Scoring.TrinketsCollected++;
		ParticleSystem CollectParticle = Instantiate (CollectedParticle, CollectTrans.position, Quaternion.identity) as ParticleSystem;
		Destroy (gameObject);
	}
}
