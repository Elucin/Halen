using UnityEngine;
using System.Collections;

public class platform_Drift : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddTorque(0, Random.Range(-1f, 1f), Random.Range(-1f, 1f), ForceMode.VelocityChange);
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
