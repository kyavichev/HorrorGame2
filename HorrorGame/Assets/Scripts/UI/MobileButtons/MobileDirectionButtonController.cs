using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class MobileDirectionButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonPressed { protected set; get; }
    public UnityEvent action;


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
        if (isButtonPressed)
        {
            action?.Invoke();
        }
    }
}
