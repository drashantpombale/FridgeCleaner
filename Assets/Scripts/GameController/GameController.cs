using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingletonGeneric<GameController>
{

    public enum GameStage
    {
        ItemsRemoved,
        ExpiredItemsDiscarded,
        SparyingDone,
        CleaningDone,
        Restocked
    }

    public List<GameStage> ThingsToDo = new List<GameStage>() { GameStage.ItemsRemoved, GameStage.ExpiredItemsDiscarded, GameStage.SparyingDone, GameStage.CleaningDone, GameStage.Restocked};

    public int expiredItems { get; private set; }
    
    public EndGameEvent endGame;

    [SerializeField]
    private GameObject FridgeSlots;

    public bool SprayFinished { get; set; }
    public bool CleanFinished { get; set; }

    private List<FridgeItemBase> unexpiredItems;

    private List<FridgeItemBase> expiredItemList;




    protected override void Awake()
    {
        base.Awake();
        Time.timeScale = 0;
        endGame = new EndGameEvent();
    }

    private void Start()
    {
        unexpiredItems = new List<FridgeItemBase>();
        expiredItemList = new List<FridgeItemBase>();

        foreach (FridgeItemBase item in GameObject.FindObjectsOfType<FridgeItemBase>()) 
        {
            if (item.isExpired)
                expiredItemList.Add(item);

            else
                unexpiredItems.Add(item);
        }
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
    
    public void ItemExpired() 
    {
        expiredItems++;
    }

    public bool AreItemsShelved() 
    {
        bool shelved = true;
        foreach (FridgeItemBase item in unexpiredItems) 
        {
            shelved &= item.wasRemoved;
        }

        return shelved;
    }

    public bool AreExpiredItemsDiscarded() 
    {
        if (expiredItemList.Count == 0)
            return true;

        return false;
    }

    public bool WasRestockDone() 
    {
        bool restocked = true;

        foreach (FridgeItemBase item in unexpiredItems) 
        {
            restocked &= item.wasRestocked;
        }

        return restocked;
    }

    public bool DiscardItem(FridgeItemBase item) 
    {
        if (expiredItemList.Contains(item)) 
        {
            expiredItemList.Remove(item);
            return true;
        }

        return false;
    }

    public void IsTaskOver(GameStage stage) 
    {
        switch (stage)
        {
            case GameStage.ItemsRemoved:
                if (AreItemsShelved())
                {
                    ThingsToDo.Remove(GameStage.ItemsRemoved);
                }
                break;
            case GameStage.ExpiredItemsDiscarded:
                if (AreExpiredItemsDiscarded())
                {
                    ThingsToDo.Remove(GameStage.ExpiredItemsDiscarded);
                }
                break;
            case GameStage.SparyingDone:
                if (SprayFinished)
                {
                    ThingsToDo.Remove(GameStage.SparyingDone);
                }
                break;
            case GameStage.CleaningDone:
                if (CleanFinished)
                {
                    ThingsToDo.Remove(GameStage.CleaningDone);
                }
                break;
            case GameStage.Restocked:
                if (WasRestockDone())
                {
                    ThingsToDo.Remove(GameStage.Restocked);
                }
                break;
        }

        if (ThingsToDo.Count == 0) 
        {
            endGame.Invoke(true);
        }

    }

    public void ResumeGame() 
    {
        Time.timeScale = 1;
    }

}
