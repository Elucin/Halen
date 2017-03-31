using UnityEngine;
using System.Collections;

public class ScoreObject : MonoBehaviour {
    float startTimer;
    const float lifetime = 3f;
    public Color color = new Color(0f, 1f, 0f, 1f);
	// Use this for initialization
	void Start () {
        GetComponent<MeshRenderer>().enabled = Options.displayHUD;
        GetComponent<TextMesh>().color = color;
        startTimer = Time.time;
	}
    
    void OnEnable()
    {
        OptionsMenu.onToggleHud += displayHud;
    }

    void OnDisable()
    {
        OptionsMenu.onToggleHud -= displayHud;
    }

    // Update is called once per frame
    void Update () {
        transform.position += Vector3.up * Time.deltaTime;
        transform.LookAt(Camera.main.transform.position);
        GetComponent<RectTransform>().localScale = new Vector3(-0.2f, 0.2f, 0.2f) * (Vector3.Distance(transform.position, Camera.main.transform.position) / 20f);

        if (color != Color.red)
        {
            if (Scoring.comboCounter <= 5f)
            {
                color = new Color(0f, 1f, 0f);
            }
            else if (Scoring.comboCounter <= 10f)
            {
                color = new Color(1f, 1f, 0f);
            }
            else if (Scoring.comboCounter <= 15f)
            {
                color = new Color(1f, 0.5f, 0f);
            }
            else if (Scoring.comboCounter <= 20f)
            {
                color = new Color(1f, 0f, 0f);
            }
        }

        GetComponent<TextMesh>().color = new Color(color.r, color.g, color.b, 1f - (Time.time - startTimer) / lifetime);

        if (Time.time - startTimer >= lifetime)
            Destroy(gameObject);
	}

    void displayHud()
    {
        GetComponent<MeshRenderer>().enabled = Options.displayHUD;
    }
}
