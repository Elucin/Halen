using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    public UnityEngine.UI.Slider sldSensitivity;
    public UnityEngine.UI.Slider sldGenAudio;
    public UnityEngine.UI.Toggle togInvertY;
    public UnityEngine.UI.Toggle togAutoRoll;
    public UnityEngine.UI.Toggle togDisplayHUD;
    public UnityEngine.UI.Slider sldMusicAudio;

    public delegate void DisplayHud();
    public static event DisplayHud onToggleHud;

    void Start()
    {
        Cancel();
    }

    public void ChangeSensitivity()
    {
        Options.mouseSensitivity = sldSensitivity.value;
    }

    public void ChangeGeneralAudio()
    {
       Options.generalAudio = sldGenAudio.value;
        AudioListener.volume = Options.generalAudio;
    }

    public void InvertY()
    {
        Options.invertY = togInvertY.isOn;
    }

    public void ChangeMusicAudio()
    {
        Options.musicAudio = sldMusicAudio.value;
    }

    public void DisplayHUD()
    {
        Options.displayHUD = togDisplayHUD.isOn;
        onToggleHud();
    }

    public void AutoRoll()
    {
        Options.autoRoll = togAutoRoll.isOn;
    }

    public void Apply()
    {
        Options.ApplySettings();
    }

    public void Cancel()
    {
        Options.LoadPrefs();
        togInvertY.isOn = Options.invertY;
        togDisplayHUD.isOn = Options.displayHUD;
        togAutoRoll.isOn = Options.autoRoll;
        sldMusicAudio.value = Options.musicAudio;
        sldGenAudio.value = Options.generalAudio;
        AudioListener.volume = Options.generalAudio;
        sldSensitivity.value = Options.mouseSensitivity;
    }
}
