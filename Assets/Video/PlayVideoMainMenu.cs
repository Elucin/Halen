using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayVideoMainMenu : MonoBehaviour {
    public static int clipIndex = 0;
    bool playingCredits = false;
    public GameObject menu;
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
        PlayBackground();

    }

    public void PlayCredits()
    {
        menu.SetActive(false);
        Renderer r = GetComponent<Renderer>();
        AudioSource a = GameObject.FindObjectOfType<AudioSource>();
        r.material = CutsceneList[1].video;
        a.clip = CutsceneList[1].audio;
        movie = (MovieTexture)r.material.mainTexture;
        movie.Play();
        playingCredits = true;
    }

    void PlayBackground()
    {
        Renderer r = GetComponent<Renderer>();
        AudioSource a = GameObject.FindObjectOfType<AudioSource>();
        r.material = CutsceneList[0].video;
        a.clip = CutsceneList[0].audio;
        movie = (MovieTexture)r.material.mainTexture;
        movie.Play();
        movie.loop = true;
    }

    void Update()
    {
        if(playingCredits && (!movie.isPlaying || Input.GetButtonDown("Cancel")))
        {
            movie.Stop();
            menu.SetActive(true);
            PlayBackground();
            playingCredits = false;
        }
    }
}