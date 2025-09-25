using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;



public class ItemSlot : MonoBehaviour, IPointerDownHandler, IDropHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    [Header("Audio")]
public AudioSource audioSource;
public AudioClip resetClip;
    public List<GameObject> collectedItems = new List<GameObject>();

    // dragging support
    private RectTransform rectTransform;
    private Transform parentAfterDrag;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    public TextMeshProUGUI basketHint;
    public bool isOn;
    public TextMeshProUGUI smellHint;
    public bool dropOn;
    public TextMeshProUGUI resetHint;
    [SerializeField] Canvas canvas;

    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        startPosition = rectTransform.anchoredPosition; // remember basket's start position
        isOn = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            collectedItems.Clear();
            resetHint.gameObject.SetActive(true);
            basketHint.text = collectedItems.Count + " ingredients inside the basket ";
            Debug.Log(collectedItems.Count + " inside the basket");
   if (audioSource != null && resetClip != null)
{
    Debug.Log("Trying to play reset sound...");
    audioSource.PlayOneShot(resetClip);
}
else
{
    Debug.LogWarning("Missing AudioSource or resetClip!");
}
 
            StartCoroutine(HideBasketHintAfterDelay(1f));
        }
    }


    // called when something is dropped into the basket
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            GameObject droppedItem = eventData.pointerDrag;

            // adds item to basket
            if(collectedItems.Count < 5)
            {
                collectedItems.Add(droppedItem);
                basketHint.text = collectedItems.Count + " ingredients inside the basket ";
                basketHint.gameObject.SetActive(true);
                dropOn = true;
            }

            else
            {
                basketHint.text = " Reached Max ingredients\n\n " + collectedItems.Count + " ingredients inside the basket ";
                basketHint.gameObject.SetActive(true);
                dropOn = true;
            }

            //collectedItems.Add(droppedItem);
            //basketHint.text = collectedItems.Count + " ingredients inside the basket";
            //basketHint.gameObject.SetActive(true);
            //dropOn = true;

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

    public void OnPointerDown(PointerEventData eventData)
    {
        //if (basketHint == null) return;
        //Debug.Log("Clicking Basket");
        smellHint.gameObject.SetActive(false);

        if (dropOn)
        {
            basketHint.gameObject.SetActive(false);
            isOn = true;
        }

        basketHint.text = collectedItems.Count + " ingredients inside the basket";

        if (isOn) //when basket hint is on, then turn off
        {
            basketHint.gameObject.SetActive(false);
            isOn = false;
            dropOn = false;
        }

        else //when basket hint is off, then turn on
        {
            basketHint.gameObject.SetActive(true);
            isOn = true;
        }

    }

    // basket dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
        basketHint.gameObject.SetActive(false);
        smellHint.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
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

    private IEnumerator HideBasketHintAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        resetHint.gameObject.SetActive(false);
    }

}



