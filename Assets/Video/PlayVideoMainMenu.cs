using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayVideoMainMenu : MonoBehaviour {
    public static int clipIndex = 0;

    [System.Serializable]
    public struct av
    {
        public string name;
        public Material video;
        public AudioClip audio;
    }

    public static string csName;
    MovieTexture movie;

    public List<av> CutsceneList = new List<av>();
    // Use this for initialization
    void Start () {
        Renderer r = GetComponent<Renderer>();
        AudioSource a = GameObject.FindObjectOfType<AudioSource>();
        r.material = CutsceneList[clipIndex].video;
        a.clip = CutsceneList[clipIndex].audio;
        movie = (MovieTexture)r.material.mainTexture;
        movie.Play();
        movie.loop = true;
	}
}