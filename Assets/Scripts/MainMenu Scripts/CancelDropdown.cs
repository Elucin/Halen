using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CancelDropdown : Dropdown, ICancelHandler, ISelectHandler {

    public UnityEvent onCancel { get; private set; }
    public UnityEvent onSelect { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        onCancel = new UnityEvent();
        onSelect = new UnityEvent();
    }
    // Use this for initialization
    void ICancelHandler.OnCancel(BaseEventData eventData)
    {
        onCancel.Invoke();
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        onSelect.Invoke();
    }
}
