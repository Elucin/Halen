using UnityEngine;
using System.Collections;

public class ChildCollision : MonoBehaviour {

    void OnCollisionEnter(Collision c)
    {
		if (transform.root.name.Contains ("Charger"))
			GetComponentInParent<AICharger> ().OnChildCollisionEnter (c);
		else if (transform.root.name.Contains ("Brawler"))
			GetComponentInParent<AIBrawler> ().OnChildCollisionEnter (c);     
    }

	void OnTriggerEnter(Collider c)
	{
		if (transform.root.name.Contains ("Halen"))
			GetComponentInParent<PlayerControl> ().OnChildCollisionEnter (c);
        else if(transform.root.name.Contains("Gunner"))
            GetComponentInParent<AIGunner>().OnChildCollisionEnter(c);
    }
}
