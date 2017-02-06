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

    void OnTriggerEnter(Collider c)
    {
		if (c.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
			GetComponent<MeshRenderer>().material = activeMat;
            //Saving.CheckpointID = ID;
            PlayerPrefs.SetInt("Checkpoint", ID);
            PlayerPrefs.SetInt("Brawlers", Scoring.brawlersKilled);
            PlayerPrefs.SetInt("Gunners", Scoring.gunnersKilled);
            PlayerPrefs.SetInt("Snipers", Scoring.snipersKilled);
            PlayerPrefs.SetInt("Chargers", Scoring.chargersKilled);
            PlayerPrefs.SetInt("Floaters", Scoring.floatersKilled);
            PlayerPrefs.SetInt("Combo", Scoring.biggestCombo);
            //Saving.Score = Scoring.PlayerScore;
            PlayerPrefs.SetInt("Score", Scoring.PlayerScore);
        }
    }
}
