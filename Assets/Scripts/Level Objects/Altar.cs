using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : LevelObject {

    public LootManager myLoot;
    public List<AltarOutcome> possOutcomes = new List<AltarOutcome>();
    public int baseAltarPrice;
    private int numberOfTimesUsed;
    public int chanceToBreak;

    public void Start()
    {
        numOfUses = -1;
        numberOfTimesUsed = 0;
    }


    public override bool UseRestrictionsMet()

    {

        if (GameManager.GetPlayer().GetComponent<Entity>().stats.GetStatModifiedValue(Constants.BaseStatType.Money) >= baseAltarPrice)
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
        numberOfTimesUsed += 1;
        StatAdjustmentManager.AddStaticPlayerStatAdjustment(Constants.BaseStatType.Money, -baseAltarPrice);
        base.ActivationFunction();
        if (possOutcomes[Random.Range(0, possOutcomes.Count)] != null)
        {
            possOutcomes[Random.Range(0, possOutcomes.Count)].Outcome(this);
        }
        else
        {
            Debug.Log("You have an empty outcome on " + gameObject.name);
        }

        float testChance =Random.Range(0, 100);
        if (testChance <= numberOfTimesUsed * chanceToBreak && numberOfTimesUsed > 1)
        {
            Debug.Log("ALTAR DONE BROKE");
            Destroy(this);
        }

    }
}
