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
        GetComponent<CancelButton>().onCancel.AddListener(delegate 
        {
            CancelCallBack(gotoButton);
        });
    }

    void CancelCallBack(Button b)
    {
        if(GetComponent<Button>().IsActive())
            b.Select();
    }
}

