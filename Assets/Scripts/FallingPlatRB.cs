using UnityEngine;
using System.Collections;

public class FallingPlatRB : MonoBehaviour {

	public Rigidbody rb;
	private bool fall = false;
	public platform_CameraShake _platCamShake;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (fall == true) {
			rb.AddForce (Physics.gravity * 0.2f);
		}
	}
		
	void OnCollisionEnter(Collision other)
	{
		if (other.transform.tag == "Player")
		{
			fall = true;
			_platCamShake.PlayShake ();
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.transform.tag == "Player")
		{
			_platCamShake.StopShake ();
		}
	}

}
