using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    Transform parentAfterDrag;
    private CanvasGroup canvasGroup;
    public TextMeshProUGUI tmpText;
    private string objectTag;
    public TextMeshProUGUI basketHint;
    public ItemSlot onSwitch;

       public AudioSource audioSource;
    public AudioClip clickSound;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
        tmpText.gameObject.SetActive(false);
        basketHint.gameObject.SetActive(false);
        onSwitch.isOn = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (tmpText == null) return;

            // play sfx when click or hold
    if (audioSource != null && clickSound != null)
    {
        audioSource.PlayOneShot(clickSound);
    }

        basketHint.gameObject.SetActive(false);
        onSwitch.isOn = false;

        tmpText.gameObject.SetActive(true);
        string objectTag = gameObject.tag;

        switch (objectTag)
        {
            case "Egg":
                tmpText.text = "It feels round and smells like nothing";
                break;

            case "Chicken":
                tmpText.text = "It's kinda soft and smells a bit raw";
                break;

            case "Onion":
                tmpText.text = "It feels round and smells like I'm gonna cry";
                break;

            case "Noodles":
                tmpText.text = "It feels thin and smells like it's been fried";
                break;

            case "Tofu":
                tmpText.text = "It feels square and soft and smells like beans";
                break;

            case "Tomato":
                tmpText.text = "It feels round and smells like ketchup";
                break;

            default:
                tmpText.text = "";
                break;
        }
    }
}