using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFandRW : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)&&Time.timeScale>0.1f)
			Time.timeScale -= 0.1f;
		if (Input.GetKey (KeyCode.RightArrow))
			Time.timeScale += 0.1f;
		if (Input.GetKey (KeyCode.DownArrow))
			Time.timeScale =1;
		
	}


}
