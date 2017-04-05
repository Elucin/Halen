using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuScript1 : MonoBehaviour {
	public Button startText;
	public Button exitText;
    public Button continueButton;

	// Use this for initialization
	void Start () {
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
        Options.LoadPrefs();
        if (PlayerPrefs.GetInt("Checkpoint", -1) == -1)  
            continueButton.interactable = false;
	}

	public void NoPress()
	{
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel()
	{
        Scoring.brawlersKilled = 0;
        Scoring.gunnersKilled = 0;
        Scoring.chargersKilled = 0;
        Scoring.floatersKilled = 0;
        Scoring.snipersKilled = 0;
        Scoring.biggestCombo = 0;
        Scoring.PlayerScore = 0;
        PlayerPrefs.SetInt("Brawlers", 0);
        PlayerPrefs.SetInt("Chargers", 0);
        PlayerPrefs.SetInt("Gunners", 0);
        PlayerPrefs.SetInt("Snipers", 0);
        PlayerPrefs.SetInt("Floaters", 0);
        PlayerPrefs.SetInt("Combo", 0);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("BrawlersL", 0);
        PlayerPrefs.SetInt("ChargersL", 0);
        PlayerPrefs.SetInt("GunnersL", 0);
        PlayerPrefs.SetInt("SnipersL", 0);
        PlayerPrefs.SetInt("FloatersL", 0);
        PlayerPrefs.SetInt("ComboL", 0);
        PlayerPrefs.SetInt("ScoreL", 0);
		PlayerPrefs.SetInt ("TrinketsL", 0);
        LoadNextScene.Level = SceneManager.GetActiveScene().buildIndex + 1;
        PlayVideo.clipIndex = 0;
        SceneManager.LoadScene("Cutscene");
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

    public void ContinueGame()
    {
        Scoring.brawlersKilled = PlayerPrefs.GetInt("Brawlers", 0);
        Scoring.chargersKilled = PlayerPrefs.GetInt("Chargers", 0);
        Scoring.gunnersKilled = PlayerPrefs.GetInt("Gunners", 0);
        Scoring.snipersKilled = PlayerPrefs.GetInt("Snipers", 0);
        Scoring.floatersKilled = PlayerPrefs.GetInt("Floaters", 0);
        Scoring.biggestCombo = PlayerPrefs.GetInt("Combo", 0);
        Scoring.PlayerScore = PlayerPrefs.GetInt("Score", 0);
		Scoring.TrinketsCollected = PlayerPrefs.GetInt ("Trinkets", 0);
        LoadNextScene.Level = PlayerPrefs.GetInt("Level", 1);
        Saving.doLoad = true;
        Saving.CP = PlayerPrefs.GetInt("Checkpoint", -1);
        SceneManager.LoadScene("Loading");
    }
}