﻿using UnityEngine;
using System.Collections;

public class ScoreObject : MonoBehaviour {
    float startTimer;
    const float lifetime = 3f;
    public Color color = new Color(0f, 1f, 0f, 1f);
	// Use this for initialization
	void Start () {
        GetComponent<TextMesh>().color = color;
        startTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.up * Time.deltaTime;
        transform.LookAt(PlayerControl.position);
        GetComponent<RectTransform>().localScale = new Vector3(-0.2f, 0.2f, 0.2f) * (Vector3.Distance(transform.position, PlayerControl.position) / 20f);
        GetComponent<TextMesh>().color = new Color(color.r, color.g, color.b, 1f - (Time.time - startTimer) / lifetime);

        if (Time.time - startTimer >= lifetime)
            Destroy(gameObject);
	}
}