using UnityEngine;
using System.Collections;

public class DelayAudio : MonoBehaviour {
    public float delay = 0;
    private float timer;
    private bool playing = false;
	// Use this for initialization
	void Start () {
        timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.time - timer >= delay && !playing)
        {
            playing = true;
            GetComponent<AudioSource>().Play();
        }
	}
}
