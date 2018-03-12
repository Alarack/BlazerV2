using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Pools")]
public class ItemPools : ScriptableObject {


    public List<Rarity> rarity = new List<Rarity>();
    public List<ItemPoolCategory> pools = new List<ItemPoolCategory>();



    public ItemData.ItemRarity DetermineRarity() {
        float roll = Random.Range(0f, 1f);

        

        Dictionary<ItemData.ItemRarity, float> dropChances = GetDropChances();

        //Debug.Log(roll + " is the roll");

        //Debug.Log(dropChances[ItemData.ItemRarity.Legendary] + " is the legend drop chance");

        if(roll <= dropChances[ItemData.ItemRarity.Legendary]) 
            return ItemData.ItemRarity.Legendary;

        if (roll <= dropChances[ItemData.ItemRarity.Rare])
            return ItemData.ItemRarity.Rare;

        if (roll <= dropChances[ItemData.ItemRarity.Common])
            return ItemData.ItemRarity.Common;


        return ItemData.ItemRarity.None;
    }



    private Dictionary<ItemData.ItemRarity, float> GetDropChances() {
        Dictionary<ItemData.ItemRarity, float> result = new Dictionary<ItemData.ItemRarity, float>();

        for(int i = 0; i <rarity.Count; i++) {
            result.Add(rarity[i].rarity, rarity[i].threshold);
        }

        return result;
    }



    public ItemData GetItem(Constants.ItemPool pool) {
        ItemData result = null;

        Debug.Log("Seeking " + pool);

        //Debug.Log(pools.Count + " is pool count");

        for (int i = 0; i < pools.Count; i++) {
            if (pools[i].itemPool == pool) {
                ItemPoolCategory target = pools[i];

                List<ItemData> unlockedItems = new List<ItemData>();

                int itemCount = target.items.Count;

                Debug.Log(itemCount + " items in " + target.items + " in pool" + pool);

                if(itemCount > 0) {
                    for (int j = 0; j < itemCount; j++) {
                        if (target.items[j].unlocked) {
                            unlockedItems.Add(target.items[j]);
                        }
                    }

                    if (unlockedItems.Count < 1) {
                        return null;
                    }

                    int randomIndex = Random.Range(0, unlockedItems.Count);

                    return unlockedItems[randomIndex];
                }
                else {
                    Debug.LogError("There are 0 items in the item pool: " + pool.ToString());
                    return null;
                }
               
            }
        }

        return result;
    }


    [System.Serializable]
    public class ItemPoolCategory {
        public Constants.ItemPool itemPool;
        public List<ItemData> items = new List<ItemData>();

    }

    [System.Serializable]
    public class Rarity {
        public ItemData.ItemRarity rarity;
        public float threshold;
    }

}