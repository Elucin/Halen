﻿using UnityEngine;
using System.Collections;

public class ChargerWeakSpot : MonoBehaviour {
    //public ParticleSystem sparks;
    public bool exposed = false;
    public GameObject[] backpanels;

	void OnCollisionEnter(Collision c)
    {
        if (c.transform.CompareTag("Player"))
        {
            PlayerControl halen = c.transform.GetComponent<PlayerControl>();
            if (halen.IsDashing() && exposed)
                GetComponentInParent<AIBase>().health = 0;
        }
        else if (c.transform.name.Contains("LargeShot"))
        {
            if (!exposed)
            {
                exposed = true;
                Destroy(backpanels[0]);
                Destroy(backpanels[1]);
                //sparks.Play();
                GetComponent<Light>().color = Color.red;
            }
        }
        else if (c.transform.name.Contains("SmallShot") && exposed)
        {
            GetComponentInParent<AIBase>().doStun(1f);
        }
    }
}