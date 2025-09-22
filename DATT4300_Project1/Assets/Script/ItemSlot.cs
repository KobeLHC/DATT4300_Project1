using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    public List<GameObject> collectedItems = new List<GameObject>();

    // dragging support
    private RectTransform rectTransform;
    private Transform parentAfterDrag;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startPosition = rectTransform.anchoredPosition; // remember basket's start position
    }

    // called when something is dropped into the basket
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject droppedItem = eventData.pointerDrag;

            // adds item to basket
            collectedItems.Add(droppedItem);

            // debug 
            string itemTag = string.IsNullOrEmpty(droppedItem.tag) ? droppedItem.name : droppedItem.tag;
            Debug.Log("Item dropped into basket: " + itemTag);

            List<string> names = new List<string>();
            foreach (GameObject item in collectedItems)
            {
                names.Add(string.IsNullOrEmpty(item.tag) ? item.name : item.tag);
            }
            string basketArray = "[ " + string.Join(", ", names) + " ]";
            Debug.Log("The basket now contains: " + basketArray);
        }
    }

    // basket dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

public void OnEndDrag(PointerEventData eventData)
{
    // get basket center in world space
    Vector2 basketWorldPos = rectTransform.position;

    // check all colliders at that point (pot is a 2D collider FOR NOW)
    Collider2D hit = Physics2D.OverlapPoint(basketWorldPos);

    if (hit != null)
    {
        Pot pot = hit.GetComponent<Pot>();
        if (pot != null)
        {
            pot.HandleBasketDrop(); // run recipe check
        }
    }

    // original position snap back
    transform.SetParent(parentAfterDrag);
    rectTransform.anchoredPosition = startPosition;
    canvasGroup.blocksRaycasts = true;
}

}


