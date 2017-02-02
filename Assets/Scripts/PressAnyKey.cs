using UnityEngine;
using System.Collections;

public class PressAnyKey : MonoBehaviour {
    float timer;
    
	// Use this for initialization
	void Start () {
        timer = Time.time;
        if (Time.timeScale < 1)
            Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.anyKeyDown && Time.time - timer > 1.0f)
        {
            Application.Quit();
        }
	}
}
