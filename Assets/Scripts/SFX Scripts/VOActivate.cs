using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOActivate : MonoBehaviour {

	public GameObject KickerVO;

	// Use this for initialization
	void Start () {
		KickerVO.SetActive (false);
	}
	
	void OnTriggerEnter(Collider c)
	{
		if (c.CompareTag("Player"))
		{
			KickerVO.SetActive (true);
		}
	}
}
