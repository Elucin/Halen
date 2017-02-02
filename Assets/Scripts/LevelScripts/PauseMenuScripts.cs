using UnityEngine;
using System.Collections;

public class PauseMenuScripts : MonoBehaviour {

    public void Exit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.sceneCount - 1);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartFromCheckpoint()
    {
        Saving.Respawn(false);
    }
}
