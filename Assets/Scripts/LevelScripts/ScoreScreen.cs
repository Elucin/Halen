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
    public Text FinalScore;
    public Text Name;
    public Text Position;
    HSController hsControl;
    public GameObject scoreUploadCanvas;
    // Use this for initialization
    void Start () {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
        hsControl = FindObjectOfType<HSController>();
       
        Brawlers.text = Scoring.brawlersKilled.ToString();
        Gunners.text = Scoring.gunnersKilled.ToString();
        Snipers.text = Scoring.snipersKilled.ToString();
        Chargers.text = Scoring.chargersKilled.ToString();
        Floaters.text = Scoring.floatersKilled.ToString();
        Combo.text = Scoring.biggestCombo.ToString();
        Total.text = Scoring.PlayerScore.ToString();
        FinalScore.text = Scoring.PlayerScore.ToString();
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
        //StartCoroutine(hsControl.PostScores(Scoring.PlayerScore));
        if (LoadNextScene.Level == 7)
        {
            Position.text = hsControl.position.ToString();
            scoreUploadCanvas.SetActive(true);
        }
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }

    public void SubmitHighscore()
    {
        if(Name.text != "")
            StartCoroutine(hsControl.PostScores(Scoring.PlayerScore, Name.text));
        else
            StartCoroutine(hsControl.PostScores(Scoring.PlayerScore));
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void Cancel()
    {

    }
}
