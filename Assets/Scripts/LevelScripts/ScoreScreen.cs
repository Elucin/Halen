using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {
    public Text Brawlers;
    public Text Gunners;
    public Text Floaters;
    public Text Snipers;
    public Text Chargers;
    public Text Combo;
    public Text Total;
    HSController hsControl;
    // Use this for initialization
    void Start () {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
        hsControl = FindObjectOfType<HSController>();
        Brawlers.text = "Brawlers Killed: " + Scoring.brawlersKilled.ToString();
        Gunners.text = "Gunners Killed: " + Scoring.gunnersKilled.ToString();
        Snipers.text = "Snipers Killed: " + Scoring.snipersKilled.ToString();
        Chargers.text = "Chargers Killed: " + Scoring.chargersKilled.ToString();
        Floaters.text = "Floaters Killed: " + Scoring.floatersKilled.ToString();
        Combo.text = "Highest Combo: " + Scoring.biggestCombo.ToString();
        Total.text = "TOTAL SCORE: " + Scoring.PlayerScore.ToString();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Restart()
    {
        Scoring.brawlersKilled = 0;
        Scoring.gunnersKilled = 0;
        Scoring.chargersKilled = 0;
        Scoring.floatersKilled = 0;
        Scoring.snipersKilled = 0;
        Scoring.biggestCombo = 0;
        Scoring.PlayerScore = 0;
        //Confirmation Window Later?
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Continue()
    {
        StartCoroutine(hsControl.PostScores(Scoring.PlayerScore));
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }
}
