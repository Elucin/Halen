using UnityEngine;
using System.Collections;

public class ChildCollision : MonoBehaviour {

    void OnCollisionEnter(Collision c)
    {
        if(transform.root.name.Contains("Charger"))
            GetComponentInParent<AICharger>().OnChildCollisionEnter(c);
        else if(transform.root.name.Contains("Brawler"))
            GetComponentInParent<AIBrawler>().OnChildCollisionEnter(c);
    }
}
