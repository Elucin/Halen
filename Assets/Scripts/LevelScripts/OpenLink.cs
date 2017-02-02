using UnityEngine;
using System.Collections;

public class OpenLink : MonoBehaviour {

	public void OpenExternalLink()
    {
        Application.OpenURL("https://goo.gl/forms/sWNGIon78fgSu89W2");
		Application.Quit();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
