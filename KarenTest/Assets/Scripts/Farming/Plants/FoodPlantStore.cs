using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlantStore
{
    public Dictionary<string, FoodPlant> foodPlantsForSale;

    public FoodPlantStore()
    {
        this.foodPlantsForSale = FoodPlantFactory.Instance.foodPlantDictionary;
    }

    public string SellFoodPlant(string key)
    {
        // Bruh idk
        
        if (GameManager.SharedInstance.farmManager.plotHandler.CanBuyFoodSeed())
        {
            GameManager.SharedInstance.farmManager.plotHandler.FillFirstFoodPlot(foodPlantsForSale[key]);
            return $"Bought a {key} seed";
        }

        return "Could not buy another plant, your food plots are full";
    }

    string PrintStock()
    {
        string message = "";

        foreach (FoodPlant fp in foodPlantsForSale.Values)
        {
            message += fp.foodKey;
        }

        return message;
    }
}
