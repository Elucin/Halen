using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
	}

}
