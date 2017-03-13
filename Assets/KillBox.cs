using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if(c.transform.CompareTag("Player"))
        {
            PlayerControl.playerControl.damageBuffer += 999;
        }
    }
}
