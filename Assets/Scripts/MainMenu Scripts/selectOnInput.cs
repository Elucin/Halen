using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class selectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected = true;

	// Use this for initialization
	void Start () {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
        eventSystem.SetSelectedGameObject(selectedObject);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(buttonSelected);
		if((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && !buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }

		if (Mathf.Sqrt(Input.GetAxis ("Camera X Mouse") + Input.GetAxis ("Camera Y Mouse")) > 0.01f) {
			eventSystem.SetSelectedGameObject(null);
			buttonSelected = false;
		}
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }

    public void SetSelected(GameObject selected)
    {
        selectedObject = selected;
        StartCoroutine(DelaySetSelected());
        buttonSelected = false;
    }

    IEnumerator DelaySetSelected()
    {
        eventSystem.SetSelectedGameObject(null);
        yield return null;
        eventSystem.SetSelectedGameObject(selectedObject);
    }
}
