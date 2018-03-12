using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : LevelObject {

    public enum ShopType
    {
        BasicShop,
        WeaponShop,
        TrinketShop,
        BlackMarket
    }

    public ShopType myType;
    public ItemData myItem;
    private LootManager myLoot;

    // Use this for initialization
    void Awake () {
        numOfUses = 1;
        myLoot = GetComponent<LootManager>();
        switch (myType)
        {
            case ShopType.BasicShop:
                myLoot.pool = Constants.ItemPool.BasicShop;
                break;
            case ShopType.WeaponShop:
                myLoot.pool = Constants.ItemPool.WeaponShop;
                break;
            case ShopType.TrinketShop:
                myLoot.pool = Constants.ItemPool.TrinketShop;
                break;
            case ShopType.BlackMarket:
                myLoot.pool = Constants.ItemPool.BlackMarket;
                break;
        }
        Debug.Log(myItem);
        myItem = GameManager.GetItemPools().GetItem(myLoot.pool);
        Debug.Log(myItem);
    }
}
