﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CancelButton : Button, ICancelHandler
{
    public UnityEvent onCancel {get; private set; }

    protected override void Awake()
    {
        base.Awake();
        onCancel = new UnityEvent();
    }
    // Use this for initialization
    void ICancelHandler.OnCancel(BaseEventData eventData)
    {
        onCancel.Invoke();
    }
}

