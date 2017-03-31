using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChunks : MonoBehaviour {
	public float minTime;
	public float maxTime;
    MeshCollider c;

	// Use this for initialization
	void Start () {
        c = GetComponent<MeshCollider>();
        StartCoroutine(Collide());
	}

    IEnumerator Collide()
    {
        yield return new WaitForSeconds(0.25f);
        c.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, Random.Range(minTime, maxTime));
	}
}
