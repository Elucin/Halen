﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    private bool isActivated = false;
    public static int numCheckpoints = 0;
    public int ID;
	public Material activeMat;
	public Material inactiveMat;
	public GameObject CheckLight;
    Jumo jumper;

    // Use this for initialization
    void Start() {
        jumper = GameObject.FindObjectOfType<Jumo>();
        ID = numCheckpoints++;
        transform.name = "Checkpoint" + ID.ToString();
    }

    void OnTriggerEnter(Collider c)
    {
		if (c.CompareTag("Player") && !isActivated)
        {
            if(jumper != null)
                jumper.CheckpointY = transform.position.y + 60f;
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
			gameObject.GetComponent < ParticleSystem > ().Play ();
			CheckLight.SetActive (true);
        }
    }
}
