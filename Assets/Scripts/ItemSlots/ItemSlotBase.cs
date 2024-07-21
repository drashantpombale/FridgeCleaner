using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotBase : MonoBehaviour, IDropHandler
{
    public bool slotOccupied { get; set; }

    private void Awake()
    {
        slotOccupied = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (ModeController.Instance.currentMode == ModeController.GameMode.Default) {
            if (eventData != null && !slotOccupied)
            {
                //If this slot is not occupied then set the position of the object to the slot's position
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            }
        }
    }
}
