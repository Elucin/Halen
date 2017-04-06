using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour {

	public ParticleSystem CollectedParticle;
	public Transform CollectTrans;
    [SerializeField]
    private UIScript ui;

    void Start()
    {
        ui = GameObject.Find("UI 1").GetComponent<UIScript>();
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
		Scoring.AddScore(transform, Scoring.comboMultiplier, 1000, 0);
        GameObject sp = ui.AddStylePoint(1000, "Trinket");
        sp.GetComponent<UnityEngine.UI.Text>().color = new Color(1f, 0.8f, 0f);
        sp.GetComponent<UnityEngine.UI.Text>().fontStyle = FontStyle.Bold;
        Scoring.TrinketsCollected++;
		ParticleSystem CollectParticle = Instantiate (CollectedParticle, CollectTrans.position, Quaternion.identity) as ParticleSystem;
		Destroy (gameObject);
	}
}
