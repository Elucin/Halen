using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    public UnityEngine.UI.Slider sldSensitivity;

	public void ChangeSensitivity()
    {
        Options.mouseSensitivity = sldSensitivity.value;
    }
}
