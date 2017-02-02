using UnityEngine;
using System.Collections;

public class ChildCollision : MonoBehaviour {

    void OnCollisionEnter(Collision c)
    {
        GetComponentInParent<AICharger>().OnChildCollisionEnter(c);
    }
}
