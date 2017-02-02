using UnityEngine;
using System.Collections;

public class LoadNextScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex + 1);
	}
}
