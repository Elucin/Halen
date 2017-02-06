using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour {
    //public static int Checkpoint = -1;
    public static int Level = 0;

	// Use this for initialization
	void Start () {
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (Level);
	}
}
