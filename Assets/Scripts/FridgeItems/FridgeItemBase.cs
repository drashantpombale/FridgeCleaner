using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Base class for fridge items
/// </summary>
public class FridgeItemBase : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Canvas canvas;

    private RectTransform rectTransform;

    private CanvasGroup canvasGroup;

    private ItemSlotBase currentSlot;

    private Vector2 resetPosition;

    private bool isExpired;

    private bool wasRemoved;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Image itemImage = GetComponent<Image>();
        float randomNumber = Random.Range(-1, 1);
        if (randomNumber < 0)
        {
            if (GameController.Instance.expiredItems < 3)
            {
                itemImage.color = Color.red;
                isExpired = true;
                GameController.Instance.ItemExpired();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ModeController.Instance.currentMode == ModeController.GameMode.Default)
        {
            //Set the item that is being dragged as transparent
            canvasGroup.alpha = 0.6f;
            //Set the raycasts off to detect droppable objects
            canvasGroup.blocksRaycasts = false;

            //Cache the current position of the draggable object to reset when requried
            resetPosition = eventData.pointerDrag.GetComponent<RectTransform>().position;

            if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<ItemSlotBase>() != null)
            {
                //If object belongs to a slot, cache it
                currentSlot = eventData.pointerEnter.GetComponent<ItemSlotBase>();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Move the object with mouse/touch
        if (ModeController.Instance.currentMode == ModeController.GameMode.Default)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (ModeController.Instance.currentMode == ModeController.GameMode.Default)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            //If no slot is detected or the slot is already occupied, then reset the position of the object
            if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<ItemSlotBase>() == null || eventData.pointerEnter.GetComponent<ItemSlotBase>().slotOccupied)
            {
                ResetPosition();
            }

            //else set the previous slot as unoccupied and set the new slot as current slot 
            else
            {
                ItemSlotBase slot = eventData.pointerEnter.GetComponent<ItemSlotBase>();

                if (currentSlot != null)
                    currentSlot.slotOccupied = false;
                currentSlot = slot;
                currentSlot.slotOccupied = true;
                wasRemoved = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    /// <summary>
    /// Reset the position of the object
    /// </summary>
    public void ResetPosition()
    {
        rectTransform.position = resetPosition;
    }
}
