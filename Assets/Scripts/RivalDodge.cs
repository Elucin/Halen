using UnityEngine;
using System.Collections;

public class RivalDodge : MonoBehaviour {
    //public Transform halen;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(PlayerControl.position);
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.transform.name.Contains("LargeShot"))
        {
			transform.root.GetComponent<AIRival>().DodgeDirection(c.transform);
        }
    }
}
