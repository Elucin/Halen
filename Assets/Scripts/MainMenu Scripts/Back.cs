using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class Back : MonoBehaviour, IPointerEnterHandler
{
    public GameObject optionsMenu;
    public GameObject MainMenu;
    bool selected;
    EventSystem es;

	// Use this for initialization
	void Start () {
        //optionsMenu = GameObject.Find("settings_panel");
        //MainMenu = GameObject.Find("mainMenu_panel");
        es = GameObject.FindObjectOfType<EventSystem>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (es.currentSelectedGameObject == gameObject)
            {
                Debug.Log(name);
                optionsMenu.transform.root.GetComponent<OptionsMenu>().Cancel();
                optionsMenu.SetActive(false);
                MainMenu.SetActive(true);
            }
        }
	}

    public void OnPointerEnter(PointerEventData p)
    {
        selected = true;
    }

    public void OnPointerExit(PointerEventData p)
    {
        selected = false;
    }
}
