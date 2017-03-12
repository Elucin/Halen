using UnityEngine;
using System.Collections;

public class FaceToPlayer : MonoBehaviour {
    Transform halen;
	// Use this for initialization
	void Start () {
        halen = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(halen);
	}
}
