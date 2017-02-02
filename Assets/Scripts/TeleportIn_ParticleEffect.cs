using UnityEngine;
using System.Collections;

public class TeleportIn_ParticleEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //transform.Rotate(0, 0, 20);
        //GetComponentInChildren<ParticleSystem>().Play();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(0, 10, 0);
        //transform.localEulerAngles += new Vector3(0, 20, 0);
        
        //transform.position += new Vector3(0, 0.1f, 0); 
	}
}
