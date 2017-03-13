using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    public UnityEngine.UI.Slider sldSensitivity;
    public UnityEngine.UI.Slider sldGenAudio;
    public UnityEngine.UI.Toggle togInvertY;
    public UnityEngine.UI.Slider sldMusicAudio;

    public void ChangeSensitivity()
    {
        Options.mouseSensitivity = sldSensitivity.value;
    }

    public void ChangeGeneralAudio()
    {
       Options.generalAudio = sldGenAudio.value;
    }

    public void InvertY()
    {
        Options.invertY = togInvertY.isOn;
    }

    public void ChangeMusicAudio()
    {
        Options.musicAudio = sldMusicAudio.value;
    }
}
