using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFactory
{
    private static FoodFactory instance;
    public static FoodFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FoodFactory();
            }

            return instance;
        }
    }

    public Dictionary<string, Food> foodDictionary;

    public FoodFactory()
    {
        foodDictionary = new Dictionary<string, Food>();
        FillDictionary();
    }

    void FillDictionary()
    {
        foodDictionary.Add("HP Berry", new Food("HP Berry", 0, 1, "Sprites/hpberry"));
        foodDictionary.Add("ATK Berry", new Food("ATK Berry", 1, 1, "Sprites/attackberry"));
        foodDictionary.Add("DEF Berry", new Food("DEF Berry", 2, 1, "Sprites/defberry"));
        foodDictionary.Add("SPD Berry", new Food("SPD Berry", 3, 1, "Sprites/speedberry"));
    }

    public Food GetFood(string value)
    {
        Food f = null;

        if (foodDictionary.ContainsKey(value))
        {
            f = foodDictionary[value];
        }
        else
        {
            Debug.Log($"{value} is not a kind of food");
        }

        return f;
    }
}
