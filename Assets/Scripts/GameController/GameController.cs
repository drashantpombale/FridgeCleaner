using UnityEngine;

public class GameController : MonoSingletonGeneric<GameController>
{
    public int expiredItems { get; private set; }

    [SerializeField]
    private GameObject FridgeSlots;

    public bool SprayFinished { get; set; }
    public bool CleanFinished { get; set; }

    public void ItemExpired() 
    {
        expiredItems++;
    }

    private void Update()
    {
        if (!FridgeSlots.activeInHierarchy) 
        {
            if(SprayFinished && CleanFinished) 
            {
                FridgeSlots.SetActive(true);
            }
        }
    }

}
