using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {
    public int sceneIndex = 0;
    public bool loadNextScene = false;
    public bool loadScoreScreen = false;
    public bool loadCutscene = false;
    public int cutsceneID = 0;
    float alpha = 0f;
    bool endScene = false;
    Image fadeToBlack;

    void Start()
    {
        fadeToBlack = GetComponentInChildren<Image>();
    }
   
    // Use this for initialization
    void Update () {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.F))
        {
            StopAllCoroutines();
            endScene = true;
        }

        if(endScene)
            LoadTheNextScene();
    }
	
	// Update is called once per frame
	void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            StopAllCoroutines();
            endScene = true;
        }
    }

	void LoadTheNextScene()
	{
        if (alpha >= 1f)
        {
            if (!loadNextScene)
            {
                LoadNextScene.Level = sceneIndex;
            }
            else
            {
                int thisIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
                LoadNextScene.Level = thisIndex + 1;
            }

            if (loadScoreScreen)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("ScoreScreen");
            }
            else if (loadCutscene)
            {
                PlayVideo.clipIndex = cutsceneID;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Cutscene");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
            }
        }
        else
        {
            alpha += 0.7f * Time.fixedDeltaTime;
            fadeToBlack.color = new Color(0f, 0f, 0f, alpha);
        }
	}
}
