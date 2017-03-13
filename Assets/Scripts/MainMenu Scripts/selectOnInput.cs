using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class selectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

	// Use this for initialization
	void Start () {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }

		if (Input.GetAxisRaw ("Camera X Mouse") > 0.5f || Input.GetAxisRaw ("Camera Y Mouse") > 0.5f) {
			eventSystem.SetSelectedGameObject(null);
			buttonSelected = false;
		}
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
