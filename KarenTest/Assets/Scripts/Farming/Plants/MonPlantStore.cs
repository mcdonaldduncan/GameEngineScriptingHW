using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonPlantStore
{
    public Dictionary<string, MonsterPlant> monPlantsForSale;

    public MonPlantStore()
    {
        this.monPlantsForSale = MonPlantFactory.Instance.monPlantDictionary;
    }

    public string SellMonPlant(string key)
    {
        if (GameManager.SharedInstance.farmManager.plotHandler.CanBuyMonSeed())
        {
            GameManager.SharedInstance.farmManager.plotHandler.FillFirstMonsterPlot(MonPlantFactory.Instance.GetMonPlant(key));
            return $"You bought a {key} seed";
        }

        return "You cannot buy a seed as you do not have any monster plots open";
    }

    string PrintStock()
    {
        string message = "";

        foreach(MonsterPlant mp in monPlantsForSale.Values)
        {
            message += mp.plantMonKey;
        }

        return message;
    }
}
