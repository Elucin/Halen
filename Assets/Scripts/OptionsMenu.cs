using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    public UnityEngine.UI.Slider sldSensitivity;
    public UnityEngine.UI.Slider sldGenAudio;
    public UnityEngine.UI.Toggle togInvertY;
    public UnityEngine.UI.Toggle togAutoRoll;
    public UnityEngine.UI.Toggle togDisplayHUD;
    public UnityEngine.UI.Slider sldMusicAudio;
    public UnityEngine.UI.Slider sldSfxAudio;
    public UnityEngine.UI.Slider sldVoiceAudio;
    public UnityEngine.Audio.AudioMixer musicMixer;
    public UnityEngine.Audio.AudioMixer sfxMixer;
    public UnityEngine.Audio.AudioMixer voiceMixer;
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
        musicMixer.SetFloat("MusicMasterVolume", Options.musicAudio);
    }

    public void ChangeSFXAudio()
    {
        Options.sfxAudio = sldSfxAudio.value;
        sfxMixer.SetFloat("SFXMasterVolume", Options.sfxAudio);
    }

    public void ChangeVoiceAudio()
    {
        Options.voiceAudio = sldVoiceAudio.value;
        voiceMixer.SetFloat("VoiceMasterVolume", Options.voiceAudio);
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
        musicMixer.SetFloat("MusicMasterVolume", Options.musicAudio);
        sfxMixer.SetFloat("SFXMasterVolume", Options.sfxAudio);
        voiceMixer.SetFloat("VoiceMasterVolume", Options.voiceAudio);
        sldSensitivity.value = Options.mouseSensitivity;
    }
}
