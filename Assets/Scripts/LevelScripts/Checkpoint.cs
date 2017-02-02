using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    private bool isActivated = false;
    public static int numCheckpoints = 0;
    public int ID;
	public Material activeMat;
	public Material inactiveMat;

    // Use this for initialization
    void Start() {
        ID = numCheckpoints++;
        transform.name = "Checkpoint" + ID.ToString();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider c)
    {
		if (c.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
			GetComponent<MeshRenderer>().material = activeMat;
            Saving.CheckpointID = ID;
            Saving.Score = Scoring.PlayerScore;
        }
    }
}
