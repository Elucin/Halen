using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zap : MonoBehaviour {
    bool doZap = false;
    public bool tutorialZap = false;
    public GameObject wirewall;
    public GameObject originalText;
    public GameObject newText;
    public GameObject[] zaps;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (wirewall.activeSelf)
        {
            if (tutorialZap)
            {
                originalText.SetActive(false);
                newText.SetActive(true);
            }
            doZap = true;
            foreach(GameObject g in zaps)
            {
                g.SetActive(true);
            }
        }
	}

    void OnTriggerStay(Collider c)
    {
        if(c.CompareTag("Player") && doZap)
        {
            PlayerControl.playerControl.damageBuffer += 1100f * Time.deltaTime;
        }
    }
}
