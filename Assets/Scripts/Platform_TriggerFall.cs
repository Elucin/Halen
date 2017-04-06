using UnityEngine;
using System.Collections;

public class Platform_TriggerFall : MonoBehaviour {

	public GameObject[] FallingPlats;
	private Rigidbody rb;
	private bool fall = false;

	// Use this for initialization
	void Start () 
	{
	}
		
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (fall == true) {
			foreach (GameObject FallingPlat in FallingPlats)
			{
				FallingPlat.GetComponent<Rigidbody>().useGravity = true;
				Destroy (FallingPlat, Random.Range(10, 15));
			}
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Player")
		{
			fall = true;
		}
	}
}
