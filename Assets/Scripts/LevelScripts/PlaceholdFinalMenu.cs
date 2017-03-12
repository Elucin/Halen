using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholdFinalMenu : MonoBehaviour {

	public void Return()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
