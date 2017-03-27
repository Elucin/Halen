using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class AddCancel : MonoBehaviour
{
    public Button gotoButton;
    EventSystem eventSystem;
    bool justSelected = false;

    void Start()
    {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
        if (GetComponent<CancelButton>())
        {
            GetComponent<CancelButton>().onCancel.AddListener(delegate
            {
                CancelCallBack(gotoButton, "Button");
            });
        }
        else if (GetComponent<CancelSlider>())
        {
            GetComponent<CancelSlider>().onCancel.AddListener(delegate
            {
                CancelCallBack(gotoButton, "Slider");
            });
        }
        else if (GetComponent<CancelDropdown>())
        {
            /*
            GetComponent<CancelDropdown>().onCancel.AddListener(delegate
            {
                CancelCallBack(gotoButton, "Dropdown");
            }); */
            
            GetComponent<CancelDropdown>().onSelect.AddListener(delegate
            {
                CancelCallBack(gotoButton, "Dropdown");
            });
        }
    }

    void CancelCallBack(Button b, string type)
    {
        if (type == "Button")
        {
            if (GetComponent<Button>().IsActive())
               StartCoroutine(DelaySelect(b));
        }
        else if(type == "Slider")
        {
            if (GetComponent<Slider>().IsActive())
                StartCoroutine(DelaySelect(b));
        }
        else if(type == "Dropdown")
        {
            if (!justSelected)
            {
                if (GetComponent<Dropdown>().IsActive())
                {
                    StartCoroutine(DelaySelect(b));
                }
            }
        }
    }

    public void selected()
    {
        Debug.Log("Select");
        StartCoroutine(Selected());
    }

    public IEnumerator Selected()
    {
        justSelected = true;
        yield return null;
        justSelected = false;
    }

    IEnumerator DelaySelect(Button b)
    {
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(b.gameObject);

    }
}

