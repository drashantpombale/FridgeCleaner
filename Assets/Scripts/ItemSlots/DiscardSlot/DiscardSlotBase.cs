using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// An IDropHandler class to discard items, like a trash can
/// </summary>
public class DiscardSlotBase : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData != null) 
        {
            if (GameController.Instance.DiscardItem(eventData.pointerDrag.GetComponent<FridgeItemBase>()))
            {
                Destroy(eventData.pointerDrag);
                Debug.Log("Object discarded");

                GameController.Instance.IsTaskOver(GameController.GameStage.ExpiredItemsDiscarded);
            }
        }
    }
}
