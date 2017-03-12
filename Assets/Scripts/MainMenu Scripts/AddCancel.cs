using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class AddCancel : MonoBehaviour
{
    public Button gotoButton;

    void Start()
    {
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
    }

    void CancelCallBack(Button b, string type)
    {
        if (type == "Button")
        {
            if (GetComponent<Button>().IsActive())
                b.Select();
        }
        else if(type == "Slider")
        {
            if (GetComponent<Slider>().IsActive())
                b.Select();
        }
    }
}

