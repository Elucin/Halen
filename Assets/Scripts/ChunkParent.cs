using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkParent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Transform[] t = GetComponentsInChildren<Transform>();
        if (t.Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
