using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private InputControl inputControl;
    private void Start()
    {
        inputControl = InputControl.Instance;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        inputControl.JumpButtonDown();
        InputParams.jumpButton = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputParams.jumpButton = false;
    }
}
