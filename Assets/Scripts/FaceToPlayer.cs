using UnityEngine;
using System.Collections;

public class FaceToPlayer : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(PlayerControl.halenGO.transform);
	}
}
