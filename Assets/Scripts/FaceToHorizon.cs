using UnityEngine;
using System.Collections;

public class FaceToHorizon : MonoBehaviour {
    Camera cam;
    public LayerMask mask;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height/2, 0));

        if (Physics.Raycast(ray, out hit, 1000f, LayerMasks.ignorePlayer, QueryTriggerInteraction.Ignore))
        {
            transform.LookAt(hit.point);
        }
        else
        {
            transform.LookAt(Camera.main.transform.TransformDirection(Vector3.forward) * 10000.0f - transform.position);
            
        }


    }
}
