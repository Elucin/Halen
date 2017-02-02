using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {
    public GameObject exit;
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            exit.SetActive(true);
        }
    }
}
