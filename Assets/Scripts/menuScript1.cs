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
        LoadNextScene.Level = PlayerPrefs.GetInt("Level", 1);
        Saving.doLoad = true;
        Saving.Checkpoint = PlayerPrefs.GetInt("Checkpoint", -1);
        SceneManager.LoadScene("Loading");
    }
}