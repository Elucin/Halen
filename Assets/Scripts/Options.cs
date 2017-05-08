using UnityEngine;
using System.Collections;

public static class Options{

	//Gameplay
    public static float mouseSensitivity = 300f;
	public static float aimSensitivity = 300f;
    public static bool invertY = false;
    public static bool autoRoll = true;
    public static bool displayHUD = true;

	//Audio
	public static float generalAudio = 1f;
	public static float musicAudio = 0f;
	public static float sfxAudio = 0f;
	public static float voiceAudio = 0f;

	//Graphics
	public static bool ambientOcclusion = true;
	public static bool windowed = false;
	public static int resolution = 16; //INCOMPLETE
	public static int quality = 4; //INCOMPLETE



    public static void ApplySettings()
    {
        PlayerPrefs.SetInt("togInvertY", System.Convert.ToInt32(invertY));
        PlayerPrefs.SetFloat("generalAudio", generalAudio);
        PlayerPrefs.SetFloat("musicAudio", musicAudio);
        PlayerPrefs.SetFloat("sfxAudio", sfxAudio);
        PlayerPrefs.SetFloat("voiceAudio", voiceAudio);
        PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
		PlayerPrefs.SetFloat("aimSensitivity", aimSensitivity);
        PlayerPrefs.SetInt("autoRoll", System.Convert.ToInt32(autoRoll));
        PlayerPrefs.SetInt("displayHUD", System.Convert.ToInt32(displayHUD));
		PlayerPrefs.SetInt ("ambientOcclusion", System.Convert.ToInt32 (ambientOcclusion));
		PlayerPrefs.SetInt("fullScreen", System.Convert.ToInt32 (windowed));
    }

    public static void LoadPrefs()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity", 300f);
		aimSensitivity = PlayerPrefs.GetFloat("aimSensitivity", 300f);
        generalAudio = PlayerPrefs.GetFloat("generalAudio", 1f);
        musicAudio = PlayerPrefs.GetFloat("musicAudio", 0f);
        sfxAudio = PlayerPrefs.GetFloat("sfxAudio", 0f);
        voiceAudio = PlayerPrefs.GetFloat("voiceAudio", 0f);
        invertY = PlayerPrefs.GetInt("togInvertY", 0) == 1;
        autoRoll = PlayerPrefs.GetInt("autoRoll", 1) == 1;
        displayHUD = PlayerPrefs.GetInt("displayHUD", 1) == 1;
		ambientOcclusion = PlayerPrefs.GetInt ("ambientOcclusion", 1) == 1;
		windowed = PlayerPrefs.GetInt ("fullScreen", 1) == 1;
    }

}
