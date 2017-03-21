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
    bool cancel;

	// Use this for initialization
	void Start () {
        //optionsMenu = GameObject.Find("settings_panel");
        //MainMenu = GameObject.Find("mainMenu_panel");
        Debug.Log("HEH?");
        es = GameObject.FindObjectOfType<EventSystem>();
	}
	
    void Update()
    {
        cancel = Input.GetButtonDown("Cancel"); 
    }

	// Update is called once per frame
	void LateUpdate () {
        if (cancel)
        {
            if (es.currentSelectedGameObject == gameObject)
            {
                if (MainMenu.name.Contains("Pause"))
                {
                    optionsMenu.GetComponentInParent<OptionsMenu>().Cancel();
                }
                else {
                    optionsMenu.transform.root.GetComponent<OptionsMenu>().Cancel();
                }

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
