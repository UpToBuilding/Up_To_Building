using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeImg(2, eventData.pointerEnter);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ChangeImg(1, eventData.pointerClick);
    }

    private void ChangeImg(int type, GameObject clickedObject) // 1 = off -> on, 2 = on -> off
    {
        if (clickedObject == gameObject)
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("UI Sources/StateUI/" + name + type);
        }
    }
}
