using UnityEngine;
using System.Collections;

public class rangeBand : MonoBehaviour {

    void Update()
    {
        if (PlayerControl.isDead)
            GetComponentInParent<AIBase>().triggerCount = 0;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
            GetComponentInParent<AIBase>().triggerCount++;
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
            GetComponentInParent<AIBase>().triggerCount--;
    }
}
