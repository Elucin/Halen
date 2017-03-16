using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumo : MonoBehaviour {
    float checkpointY = -60f;
    bool resetCharge = false;
    public float CheckpointY {
        get { return checkpointY; }
        set { if (value > checkpointY) { checkpointY = value; } }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if(resetCharge && !PlayerControl.IsDashing())
        {
            resetCharge = false;
            PlayerControl.Charged = false;
        }
	}

    void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            PlayerControl.Charged = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            resetCharge = true;
        }
    }
}
