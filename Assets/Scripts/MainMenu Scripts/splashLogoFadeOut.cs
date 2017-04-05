﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashLogoFadeOut : MonoBehaviour {
    public float alphaLevel = 1.0f;
    bool runAnim = false;
    GameObject mainMenu;
    bool mainMenuDisplay = false;

    void Start()
    {
        mainMenu = GameObject.Find("MainMenu");
    }
    void Update()
    {
        if(Input.anyKeyDown)
        {
            runAnim = true;
        }
        if (runAnim)
        {
            if(alphaLevel >= 0)
                alphaLevel -= 0.9f * Time.deltaTime;
            else
                mainMenuDisplay = true;
        }

        if (mainMenuDisplay)
            mainMenu.GetComponent<Canvas>().enabled = true; ;

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaLevel);
    }
}