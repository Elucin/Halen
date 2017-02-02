﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineScript : MonoBehaviour {
    List<Collider> Colliders = new List<Collider>();
    public bool triggered = false;
    bool armed;
    public Material armedMat;
    public Material triggeredMat;
    float explodeTimer;
    const float EXPLODE_TIME = 0.5f;
    public GameObject explosion;
    public SphereCollider explosionTrigger;
    float t = 0f;
    const float MINE_EXPLODE_RADIUS = 13f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(triggered)
        {
            explosionTrigger.radius = Mathf.Lerp(explosionTrigger.radius, MINE_EXPLODE_RADIUS, t);
            if (t < 1)
            {
                t = Time.deltaTime / EXPLODE_TIME;
            }
            GetComponent<MeshRenderer>().material = triggeredMat;
        }
        else if (armed)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<MeshRenderer>().material = armedMat;
        }

        if (triggered && Time.time - explodeTimer > EXPLODE_TIME)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            foreach (Collider c in Colliders)
            {
                if (c)
                {
                    if (c.transform.tag == "Player")
                        c.GetComponent<PlayerControl>().damageBuffer += 50f;
                    else if (c.transform.tag == "Enemy")
                    {
                        c.GetComponent<AIBase>().health = 0;
                        c.GetComponent<AIBase>().stylePoints.deathType = "FloaterMine";
                    }
                    else
                    {
                        c.GetComponent<MineScript>().triggered = true;
                        c.GetComponent<MineScript>().explodeTimer = Time.time - 0.4f;
                    }
                

                c.GetComponent<Rigidbody>().AddExplosionForce(80000f, transform.position - new Vector3(0, 0.67f, 0), 12f);
                }
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        
        if(c.CompareTag("Player"))
        {
            if(!Colliders.Contains(c))
                Colliders.Add(c);

            if (armed)
            {
                triggered = true;
                explodeTimer = Time.time;
            }
        }
        else if(c.CompareTag("Enemy") || c.CompareTag("Mine"))
        {
            if (!Colliders.Contains(c))
                Colliders.Add(c);
        }

    }

    void OnTriggerExit(Collider c)
    {
        if (Colliders.Contains(c))
            Colliders.Remove(c);
    }

    void OnCollisionEnter(Collision c)
    {
        armed = true;
        if (c.transform.tag == "Player")
        {
            triggered = true;
        }
    }


}