using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slots : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
    if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
