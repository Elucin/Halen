using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSequence : MonoBehaviour {

    public AudioClip aClip;
	halenEyes_Script eyeScript;
	public halenEyes_Script.EyeStruct[] EyeStep;
    public bool interrupt = true;
    public float delay = 0f;

	// Use this for initialization
	void Start () {
		eyeScript = GameObject.FindObjectOfType<halenEyes_Script> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider c) {
		if (c.CompareTag ("Player")) {
            StartCoroutine(RunSequence(c, delay));
		}
	}

    IEnumerator RunSequence(Collider c, float d)
    {
        yield return new WaitForSeconds(delay);
        if (aClip != null)
        {
            AudioSource a = c.GetComponent<AudioSource>();
            if (interrupt || !a.isPlaying)
            {
                a.clip = aClip;
                a.Play();
                eyeScript.RunSequence(EyeStep);
                Destroy(gameObject);
            }
            else
                StartCoroutine(RunSequence(c, 0.5f));
        }
    }


}
