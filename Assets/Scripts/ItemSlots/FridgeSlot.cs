using UnityEngine;
using UnityEngine.EventSystems;

public class FridgeSlot : ItemSlotBase
{
    protected override void DropItemInSlot(GameObject item)
    {
        base.DropItemInSlot(item);

        item.GetComponent<FridgeItemBase>().Restocked();
        GameController.Instance.IsTaskOver(GameController.GameStage.Restocked);

    }
}
