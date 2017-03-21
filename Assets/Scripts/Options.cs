using UnityEngine;
using System.Collections;

public static class Options{

    public static float mouseSensitivity = 300f;
    public static float generalAudio = 1f;
    public static bool invertY = false;
    public static float musicAudio = 1f;
    public static bool autoRoll = true;
    public static bool displayHUD = true;

    public static void ApplySettings()
    {
        PlayerPrefs.SetInt("togInvertY", System.Convert.ToInt32(invertY));
        PlayerPrefs.SetFloat("generalAudio", generalAudio);
        PlayerPrefs.SetFloat("musicAudio", musicAudio);
        PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
        PlayerPrefs.SetInt("autoRoll", System.Convert.ToInt32(autoRoll));
        PlayerPrefs.SetInt("displayHUD", System.Convert.ToInt32(displayHUD));
    }

    public static void LoadPrefs()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");
        generalAudio = PlayerPrefs.GetFloat("generalAudio");
        musicAudio = PlayerPrefs.GetFloat("musicAudio");
        invertY = PlayerPrefs.GetInt("togInvertY") == 1;
        autoRoll = PlayerPrefs.GetInt("autoRoll") == 1;
        displayHUD = PlayerPrefs.GetInt("displayHUD") == 1;
    }

}
