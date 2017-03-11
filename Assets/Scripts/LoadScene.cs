using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {
    public int sceneIndex = 0;
    public bool loadNextScene = false;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider c)
    {
        if(c.tag == "Player")
        {
            StopAllCoroutines();
            if(!loadNextScene)
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
            else
            {
                int thisIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
                if(thisIndex >= UnityEngine.SceneManagement.SceneManager.sceneCount)
                    UnityEngine.SceneManagement.SceneManager.LoadScene(thisIndex + 1);
            }
        }
    }
}
