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

    [SerializeField] Canvas canvas;

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
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
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
                tmpText.text = "The scent of this ingredient is very subtle - almost nonexistent. You seem to pick up on a hint of sulfur, but that’s all";
                break;

            case "Chicken":
                tmpText.text = "This ingredient smells slightly meaty but is mostly neutral smelling. You think you can smell some iron from blood and a slight egg-like scent";
                break;

            case "Onion":
                tmpText.text = "Harsh on the nose, this ingredient smells sharp and pungent, as if your nose ate something spicy. But the more you sniff, the scent turns sweeter and slightly earthy, like grass with a tangy kick";
                break;

            case "Noodles":
                tmpText.text = "This ingredient doesn’t have much of a scent at all, but you feel something powdery enter your nostrils as you continue to sniff. Huh, quite grain-like";
                break;

            case "Tofu":
                tmpText.text = "This scent is quite mild, almost watery or bland. Focusing harder, you seem to notice a slightly bean-like aroma and a hint of soy";
                break;

            case "Tomato":
                tmpText.text = "The scent of this ingredient is quite complex, with many different odors. Some smell like detergent, some like geraniums. You feel some leaves as well, which smells like the definition of grassy with a hint of sweetness";
                break;

            default:
                tmpText.text = "";
                break;
        }
    }
}