﻿using UnityEngine;
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
        SceneManager.LoadScene("Loading");
    }
}