using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoneyOrKeys : AltarOutcome {

    public int maxMoney;
    public int maxKeys;
    public int percentChanceMoneyOverKeys;
    public int maxWorthPerOutcome;
    public int pricePerKey;
    public int pricePerMoney;


    public override void Outcome(Altar altar)
    {
        base.Outcome(altar);

        /*--True = Money
         *  False = Keys--*/
        bool moneyOrKeys = true;
        float moneyKeysCheck = Random.Range(0f, 1f);
        if (moneyKeysCheck >= percentChanceMoneyOverKeys / 100f)/*--Changeable percentage of money over keys--*/
        {
            moneyOrKeys = true;
        }
        else
        {
            moneyOrKeys = false;
        }

        int worth = Random.Range(0, maxWorthPerOutcome);
        if (moneyOrKeys)
        {
            if (worth / pricePerMoney <= maxMoney)
            {
            }
            else
            {
            }
        }
        else
        {
            if (worth / pricePerKey <= maxKeys)
            {
            }
            else
            {
            }
        }
    }
}
