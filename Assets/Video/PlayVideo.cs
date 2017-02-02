using UnityEngine;
using System.Collections;

public class PlayVideo : MonoBehaviour {
    MovieTexture movie;
    // Use this for initialization
    void Start () {
        Renderer r = GetComponent<Renderer>();
        movie = (MovieTexture)r.material.mainTexture;
        movie.Play();
	}

    void Update()
    {
 
        if(!movie.isPlaying)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
	

}
