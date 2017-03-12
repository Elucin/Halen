using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireBreak : MonoBehaviour {
    public GameObject unbroken;
    public GameObject broken;
    public GameObject particle;
    void OnCollisionEnter(Collision c)
    {
        if(c.transform.name.Contains("LargeShot"))
        {
            Debug.Log("Stuff");
            unbroken.SetActive(false);
            broken.SetActive(true);
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
