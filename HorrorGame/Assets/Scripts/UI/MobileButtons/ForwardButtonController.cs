using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ForwardButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonPressed { protected set; get; }
    public MobileControlsManager controlsManager;


    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }


    protected void Update()
    {
        if(isButtonPressed)
        {
            controlsManager.OnForwardButtonPressed();
        }
    }
}
