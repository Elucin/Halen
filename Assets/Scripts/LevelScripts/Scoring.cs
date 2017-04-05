using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class Scoring{

    public const float TIMER_LENGTH = 3.0f;
    public static int comboCounter = 0;
    private static int playerScore = 0;
    private static GameObject scorePrefab = Resources.Load("ScoreObject") as GameObject;
    public static float comboTimer = Time.time - TIMER_LENGTH;
    public static float comboMultiplier;

    public static int brawlersKilled = PlayerPrefs.GetInt("Brawlers", 0);
    public static int chargersKilled = PlayerPrefs.GetInt("Chargers", 0);
    public static int gunnersKilled = PlayerPrefs.GetInt("Gunners", 0);
    public static int snipersKilled = PlayerPrefs.GetInt("Snipers", 0);
    public static int floatersKilled = PlayerPrefs.GetInt("Floaters", 0);
    public static int biggestCombo = PlayerPrefs.GetInt("Combo", 0);

	public static int TrinketsCollected = PlayerPrefs.GetInt("Trinkets", 0);


    public static int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = value; }
    }
    
    public static void UpdateCombo()
    {

        if (Time.time - comboTimer >= TIMER_LENGTH)
        {
            if (comboCounter > biggestCombo)
                biggestCombo = comboCounter;
            comboCounter = 0;
        }
        comboMultiplier = (1 + comboCounter / 10f);
    }

    public static void AddScore(Transform trans, float combo, int basePoints, int additionalPoints)
    {
        comboTimer = Time.time;
        int totalAddedScore;
        //Combo stuff?
        if (basePoints > 0)
        {
            totalAddedScore = (int)(basePoints * comboMultiplier + additionalPoints);
            comboCounter++;
        }
        else
        {
            totalAddedScore = basePoints;

        }


		playerScore += totalAddedScore;
		playerScore = Mathf.Clamp (playerScore, 0, 9999999);
        //Instantiate Score at Position
        DoScoreUI(trans, totalAddedScore);
        
    }

    private static void DoScoreUI(Transform trans, float points)
    {
        GameObject scoreObject = GameObject.Instantiate(scorePrefab, trans.position + Vector3.up, Quaternion.identity) as GameObject;
        if (points > 0)
        {
            scoreObject.GetComponent<ScoreObject>().color = Color.green;
            scoreObject.GetComponent<TextMesh>().text = "+" + points.ToString();
        }
        else
        {
            scoreObject.GetComponent<ScoreObject>().color = Color.red;
            scoreObject.GetComponent<TextMesh>().text = "-" + Mathf.Abs(points).ToString();
        }
    }

    public static void ResetScore()
    {
        PlayerScore = PlayerPrefs.GetInt("ScoreL", 0);
        gunnersKilled = PlayerPrefs.GetInt("GunnersL", 0);
        floatersKilled = PlayerPrefs.GetInt("FloatersL", 0);
        brawlersKilled = PlayerPrefs.GetInt("BrawlersL", 0);
        chargersKilled = PlayerPrefs.GetInt("ChargersL", 0);
        biggestCombo = PlayerPrefs.GetInt("ComboL", 0);
        snipersKilled = PlayerPrefs.GetInt("SnipersL", 0);
		TrinketsCollected = PlayerPrefs.GetInt ("TrinketsL", 0);
		PlayerPrefs.SetInt ("Trinkets", PlayerPrefs.GetInt ("TrinketsL", 0));
        PlayerPrefs.SetInt("Brawlers", PlayerPrefs.GetInt("BrawlersL", 0));
        PlayerPrefs.SetInt("Chargers", PlayerPrefs.GetInt("ChargersL", 0));
        PlayerPrefs.SetInt("Gunners", PlayerPrefs.GetInt("GunnersL", 0));
        PlayerPrefs.SetInt("Snipers", PlayerPrefs.GetInt("SnipersL", 0));
        PlayerPrefs.SetInt("Floaters", PlayerPrefs.GetInt("FloatersL", 0));
        PlayerPrefs.SetInt("Combo", PlayerPrefs.GetInt("ComboL", 0));
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("ScoreL", 0));
}
    /*
    public static IEnumerator Bump(float targetY, Transform t)
    {
        while (t.localPosition.y > targetY && t != null)
        {
            t.localPosition = Vector3.Lerp(t.localPosition, new Vector3(0, targetY, 0), Time.deltaTime * 3);
            yield return null;
        }
    }*/
}
