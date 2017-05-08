using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    public UnityEngine.UI.Slider sldSensitivity;
	public UnityEngine.UI.Slider sldAimSensitivity;
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
	public UnityEngine.UI.Toggle togAO;
	public UnityEngine.UI.Toggle togFullScreen;

    void Start()
    {
        Cancel();
    }

	public void FullScreen()
	{
		Options.windowed = togFullScreen.isOn;
		Screen.fullScreen = !togFullScreen.isOn;
	}

    public void ChangeSensitivity()
    {
        Options.mouseSensitivity = sldSensitivity.value;
    }

	public void ChangeAimSensitivity()
	{
		Options.aimSensitivity = sldAimSensitivity.value;
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

	public void AmbientOcclusion()
	{
		Options.ambientOcclusion = togAO.isOn;
		UnityStandardAssets.ImageEffects.ScreenSpaceAmbientOcclusion.enabledAO = togAO.isOn;
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
		sldVoiceAudio.value = Options.voiceAudio;
        AudioListener.volume = Options.generalAudio;
        musicMixer.SetFloat("MusicMasterVolume", Options.musicAudio);
        sfxMixer.SetFloat("SFXMasterVolume", Options.sfxAudio);
        voiceMixer.SetFloat("VoiceMasterVolume", Options.voiceAudio);
        sldSensitivity.value = Options.mouseSensitivity;
		sldAimSensitivity.value = Options.aimSensitivity;
		togAO.isOn = Options.ambientOcclusion;
		togFullScreen.isOn = Options.windowed;
		Screen.fullScreen = !Options.windowed;
    }
}
