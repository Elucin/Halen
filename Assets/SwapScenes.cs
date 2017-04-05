using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScenes : MonoBehaviour {

    const float HIGHSCORE_DISPLAY_TIME = 60f;
    float hsStartTime;
    MovieTexture movie;

    [System.Serializable]
    public struct Video
    {
        public Material video;
        public AudioClip audio;
    }
    public Video vid;

    // Use this for initialization
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

	void Start () {
        if (SceneManager.GetActiveScene().name == "HighScoreScreen")
        {
            hsStartTime = Time.time;
        }
        else
        {
            Renderer r = GetComponent<Renderer>();
            AudioSource a = GameObject.FindObjectOfType<AudioSource>();
            r.material = vid.video;
            a.clip = vid.audio;
            movie = (MovieTexture)r.material.mainTexture;
            movie.Play();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene().name == "HighScoreScreen")
        {
            if(Time.time - hsStartTime > HIGHSCORE_DISPLAY_TIME)
            {
                SceneManager.LoadScene("VideoScene");
            }
        }
        else
        {
            if(!movie.isPlaying)
            {
                SceneManager.LoadScene("HighScoreScreen");
            }
        }
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}
