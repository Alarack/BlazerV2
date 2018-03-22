using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoneyOrKeys : AltarOutcome {

    public int maxMoney;
    public int maxKeys;
    public int percentChanceMoneyOverKeys;
    public int maxWorthPerOutcome;
    public int minWorthPerOutcome;
    public int pricePerKey;
    public int pricePerMoney;


    public override void Outcome(Altar altar)
    {
        base.Outcome(altar);

        /*--True = Money
         *  False = Keys--*/
        int keys = 0;
        int money = 0;
        bool moneyOrKeys = true;
        float moneyKeysCheck = Random.Range(0f, 1f);
        if (moneyKeysCheck <= percentChanceMoneyOverKeys / 100f)/*--Changeable percentage of money over keys--*/
        {
            moneyOrKeys = true;
        }
        else
        {
            moneyOrKeys = false;
        }

        int worth = Random.Range(minWorthPerOutcome, maxWorthPerOutcome);
        Debug.Log(worth);
        if (moneyOrKeys)
        {
            if (worth >= pricePerMoney)
            {
                if (worth / pricePerMoney < maxMoney)
                {
                    money = CalculateMoney(worth);
                }
                else
                {
                    money = maxMoney;
                    worth -= maxMoney * pricePerMoney;
                    keys = CalculateKeys(worth);
                }
            }
            else
            {
                if (CalculateKeys(worth) >= 1)
                {
                    keys = CalculateKeys(worth);
                }
            }
            SpawnMoneyAndKeys(money, keys);
        }
        else
        {
            Debug.Log("Keys");
            if (worth >= pricePerKey)
            {
                keys = CalculateKeys(worth);
                if (worth / pricePerKey < maxKeys)
                {
                }
                else
                {
                    keys = maxKeys;
                    worth -= maxKeys * pricePerKey;
                    money = CalculateMoney(worth);
                }
            }
            else
            {
                if (CalculateMoney(worth) >= 1)
                {
                    money = CalculateMoney(worth);
                }
            }
            SpawnMoneyAndKeys(money, keys);
        }
    }

    private int CalculateMoney(float worthToSpend)
    {
        return Mathf.RoundToInt((worthToSpend / pricePerMoney));
    }
    private int CalculateKeys(float worthToSpend)
    {
        return Mathf.RoundToInt((worthToSpend / pricePerKey));
    }

    private void SpawnMoneyAndKeys(float money, float keys)
    {
        Debug.Log("Spawned money and keys " + money + " " + keys);
    }
}
