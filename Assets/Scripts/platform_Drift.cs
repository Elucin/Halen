using UnityEngine;
using System.Collections;

public class platform_Drift : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddTorque(Random.Range(-0.04f, 0.04f), Random.Range(-0.04f, 0.04f), Random.Range(-0.04f, 0.04f), ForceMode.VelocityChange);
    }
}
