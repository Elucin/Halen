using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {
    bool Played = false;
	void OnTriggerEnter(Collider c)
    {
        if (!Played)
        {
            if (c.CompareTag("Player"))
            {
                GetComponent<Animation>().Play();
                Played = true;
            }
        }
    }
}
