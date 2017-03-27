using UnityEngine;
using System.Collections;

public class OpenLink : MonoBehaviour {

	public void OpenExternalLink()
    {
		Application.OpenURL("https://goo.gl/forms/CtmFWpd6Ysl0o6663");
		Application.Quit();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
