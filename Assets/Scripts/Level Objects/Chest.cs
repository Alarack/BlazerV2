using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : LevelObject {

    public enum ChestType
    {
        Box = 0,
        OneKey = 1,
        TwoKey = 2,
        ThreeKey = 3,
    }
    public ChestType myType;
    private LootManager myLoot;

    public void Awake()
    {
        myLoot = GetComponent<LootManager>();
        numOfUses = 1;
        switch (myType)
        {
            case ChestType.Box:
                myLoot.pool = Constants.ItemPool.Container;
                break;
            case ChestType.OneKey:
                myLoot.pool = Constants.ItemPool.StandardChest;
                break;
            case ChestType.TwoKey:
                myLoot.pool = Constants.ItemPool.AdvancedChest;
                break;
            case ChestType.ThreeKey:
                myLoot.pool = Constants.ItemPool.ArtifactChest;
                break;
        }
    }

    public void FixedUpdate()
    {
        //Debug.Log(UseRestrictionsMet());
    }

    public override bool UseRestrictionsMet()
    {
        if (GameManager.GetPlayer().GetComponent<Entity>().stats.GetStatModifiedValue(Constants.BaseStatType.Keys) >= (int)myType)
        {
            return base.UseRestrictionsMet();
        }
        else
        {
            return false;
        }
    }

    public override void ActivationFunction()
    {
        StatAdjustmentManager.AddStaticPlayerStatAdjustment(Constants.BaseStatType.Keys, -(int)myType);
        Debug.Log("Opened a " + myType + " Chest");
        base.ActivationFunction();
    }
}
