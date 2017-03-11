using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {
    public int sceneIndex = 0;
    public bool loadNextScene = false;
    public bool loadScoreScreen = false;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            StopAllCoroutines();
            if (!loadNextScene && !loadScoreScreen)
                LoadNextScene.Level = sceneIndex;
            else if (loadNextScene)
            {
                int thisIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
                if (thisIndex <= UnityEngine.SceneManagement.SceneManager.sceneCount)
                    LoadNextScene.Level = thisIndex + 1;
            }
            else if (loadScoreScreen)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Score Screen");
            }
        }
    }
}
