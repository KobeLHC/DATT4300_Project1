using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if(eventData.pointerDrag != null) //to confirm what drop in the basket
        {
            Debug.Log("Item " + eventData.pointerDrag.tag + " is drop");
        }
    }
}
