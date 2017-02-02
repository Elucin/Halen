using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashFade : MonoBehaviour {

	public Image splashImage;
	public string loadLevel;

	IEnumerator Start ()
	{
		splashImage.canvasRenderer.SetAlpha (0.0f);

		FadeIn ();
		yield return new WaitForSeconds (2.5f);
		FadeOut ();
		yield return new WaitForSeconds (2.5f);
		SceneManager.LoadScene (loadLevel);
	}

	void FadeIn()
	{
		//set alpha to 1, fades over 1.5 seconds
		splashImage.CrossFadeAlpha (1.0f, 1.5f, false);
	}

	void FadeOut()
	{
		//set alpha to 0, fades over 2.5 seconds
		splashImage.CrossFadeAlpha (0.0f, 2.5f, false);
	}
}