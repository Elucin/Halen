using UnityEngine;
using System.Collections;

public class SpawnTrigger : MonoBehaviour {
    Spawner s;

    void Start()
    {
        s = GetComponentInParent<Spawner>();
    }

	void OnTriggerEnter(Collider c)
    {
        s.OnChildTriggerEnter(c);
    }

    void OnTriggerExit(Collider c)
    {
        s.OnChildTriggerExit(c);
    }
}
