using UnityEngine;
using UnityEngine.EventSystems;

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

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Set the item that is being dragged as transparent
        canvasGroup.alpha = 0.6f;
        //Set the raycasts off to detect droppable objects
        canvasGroup.blocksRaycasts = false;

        //Cache the current position of the draggable object to reset when requried
        resetPosition = eventData.pointerDrag.GetComponent<RectTransform>().position;

        if (eventData.pointerEnter!= null  && eventData.pointerEnter.GetComponent<ItemSlotBase>()!= null) 
        {
            //If object belongs to a slot, cache it
            currentSlot = eventData.pointerEnter.GetComponent<ItemSlotBase>();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Move the object with mouse/touch
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        ItemSlotBase slot = eventData.pointerEnter.GetComponent<ItemSlotBase>();

        //If no slot is detected or the slot is already occupied, then reset the position of the object
        if (eventData.pointerEnter == null || slot == null || slot.slotOccupied)
        {
            ResetPosition();
        }

        //else set the previous slot as unoccupied and set the new slot as current slot 
        else 
        {
            if(currentSlot!=null)
                currentSlot.slotOccupied = false;
            currentSlot = slot;
            currentSlot.slotOccupied = true;
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
