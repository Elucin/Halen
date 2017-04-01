using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {
    public int sceneIndex = 0;
    public bool loadNextScene = false;
    public bool loadScoreScreen = false;
    public bool loadCutscene = false;
    public int cutsceneID = 0;
   
    // Use this for initialization
    void Update () {
		if (Input.GetKey (KeyCode.LeftControl) && Input.GetKey (KeyCode.F))
			LoadTheNextScene ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
			LoadTheNextScene ();
        }
    }

	void LoadTheNextScene()
	{
		StopAllCoroutines();
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
		else if(loadCutscene)
		{
			PlayVideo.clipIndex = cutsceneID;
			UnityEngine.SceneManagement.SceneManager.LoadScene("Cutscene");
		}
		else
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
		}
	}
}
