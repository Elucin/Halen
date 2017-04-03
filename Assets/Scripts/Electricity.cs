using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour {
    LineRenderer line;
    ParticleSystem p;
    ParticleSystem.Particle[] particlePos;
    public Transform Source;
    public Transform Target;
    public float Threshold;
    public bool HalenZap = true;
    // Use this for initialization
    void Start () {
        p = GetComponent<ParticleSystem>();
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        if(HalenZap)
            Target = GameObject.Find("sword_base").transform;
	}
	
	// Update is called once per frame
	void Update () {
        InitializeIfNeeded();

        if(Source != null)
            transform.position = Source.position;

        ParticleSystem.ShapeModule s = p.shape;
        Vector3[] pos = new Vector3[p.particleCount];
        p.GetParticles(particlePos);
		if (Target != null) {
			if (Vector3.Distance (transform.TransformPoint (Source.position), transform.TransformPoint (Target.root.position)) < Threshold && (!HalenZap || !PlayerControl.isDead)) {
				s.length = 2 * Vector3.Distance (transform.TransformPoint (Source.position), transform.TransformPoint (Target.position));
				transform.LookAt (Target.position);
				s.angle = 0;
				line.numPositions = p.particleCount + 2;
				line.SetPosition (line.numPositions - 1, transform.InverseTransformPoint (Target.transform.position));
			}
			else {
				s.length = 20f;
				transform.LookAt (transform.position + Vector3.up);
				s.angle = 20;
				line.numPositions = p.particleCount + 1;
			}
		}
		else {
			s.length = 20f;
			transform.LookAt (transform.position + Vector3.up);
			s.angle = 20;
			line.numPositions = p.particleCount + 1;
		}
            
        for (int i = 0; i < p.particleCount; i++)
        {
            pos[i] = particlePos[i].position;
        }

        bool sorted = true;

        do
        {
            sorted = true;
            for (int i = 0; i < pos.Length; i++)
            {
                if (i + 1 <= pos.Length - 1)
                {
                    if (pos[i].z > pos[i + 1].z)
                    {
                        Vector3 storage = pos[i];
                        pos[i] = pos[i + 1];
                        pos[i + 1] = storage;
                        sorted = false;
                    }
                }
            }
        }while (!sorted);
        line.SetPosition(0, transform.InverseTransformPoint(Source.position));
        for (int i = 0; i < p.particleCount; i++)
        {
            line.SetPosition(i + 1, pos[i]);
        }      
    }

    void InitializeIfNeeded()
    {
        if(p == null)
        {
            p = GetComponent<ParticleSystem>();
        }

        particlePos = new ParticleSystem.Particle[p.particleCount];

        if(Target == null && !PlayerControl.isDead && HalenZap)
        {
            Target = GameObject.Find("sword_base").transform;
        }
    }
}