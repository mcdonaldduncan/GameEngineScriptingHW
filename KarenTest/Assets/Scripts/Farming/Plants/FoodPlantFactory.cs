using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlantFactory
{
    private static FoodPlantFactory instance;
    public static FoodPlantFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FoodPlantFactory();
            }

            return instance;
        }
    }

    public Dictionary<string, FoodPlant> foodPlantDictionary;

    public FoodPlantFactory()
    {
        foodPlantDictionary = new Dictionary<string, FoodPlant>();
        FillDictionary();
    }

    void FillDictionary()
    {
        foodPlantDictionary.Add("HP Berry", new FoodPlant("HP Berry"));
        foodPlantDictionary.Add("ATK Berry", new FoodPlant("ATK Berry"));
        foodPlantDictionary.Add("DEF Berry", new FoodPlant("DEF Berry"));
        foodPlantDictionary.Add("SPD Berry", new FoodPlant("SPD Berry"));
    }

    public FoodPlant GetFoodPlant(string value)
    {
        FoodPlant fp = null;

        if (foodPlantDictionary.ContainsKey(value))
        {
            fp = foodPlantDictionary[value];
        }
        else
        {
            Debug.Log($"{value} is not a food plant");
        }

        return fp;
    }
}
