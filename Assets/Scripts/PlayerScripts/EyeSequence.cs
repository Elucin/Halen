using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeSequence : MonoBehaviour {

<<<<<<< HEAD
	public halenEyes_Script eyeScript;
=======
    public AudioClip aClip;
	halenEyes_Script eyeScript;
>>>>>>> refs/remotes/origin/master
	public halenEyes_Script.EyeStruct[] EyeStep;
    public bool interrupt = true;
    public float delay = 0f;

	public bool triggered = false; 

	// Use this for initialization
	void Start () {
		if (eyeScript == null) {
			eyeScript = GameObject.FindObjectOfType<halenEyes_Script> ();
		}
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider c) {
		if (c.CompareTag ("Player")) {
            StartCoroutine(RunSequence(c, delay));
		}
	}
	void Update() {
		if(triggered)
		{
			eyeScript.RunSequence (EyeStep);
			Destroy (gameObject);
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
