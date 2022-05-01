using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPlant : Plant
{
    public string foodKey;
    public Food food;

    public FoodPlant(string key)
    {
        foodKey = key;
        this.food = FoodFactory.Instance.GetFood(key);
        this.plantEvo = new PlantEvo();
    }
}
