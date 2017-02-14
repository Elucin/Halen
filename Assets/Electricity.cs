using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour {
    LineRenderer line;
    ParticleSystem p;
    ParticleSystem.Particle[] particlePos;
    public Transform Source;
    public Transform Target;
    public float Threshold = 5f;
    // Use this for initialization
    void Start () {
        p = GetComponent<ParticleSystem>();
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        InitializeIfNeeded();

        if(Source != null)
            transform.position = Source.position;

        ParticleSystem.ShapeModule s = p.shape;
        Vector3[] pos = new Vector3[p.particleCount];
        p.GetParticles(particlePos);

        if (Vector3.Distance(Source.position, Target.position) < Threshold && Target != null)
        {
            Debug.Log("In range");
            s.length = Vector3.Distance(Source.position, Target.position);
            transform.LookAt(Target.position);
            s.angle = 0;
            line.numPositions = p.particleCount + 2;
            line.SetPosition(line.numPositions - 1, transform.InverseTransformPoint(Target.transform.position));
        }
        else
        {
            s.length = 5f;
            transform.LookAt(transform.position + Vector3.up);
            s.angle = 25;
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
        } while (!sorted);


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
    }
}
