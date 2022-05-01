using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityFood : MonoBehaviour
{
    public Food food;
    public string foodKey;

    // Start is called before the first frame update
    void Start()
    {
        food = FoodFactory.Instance.GetFood(foodKey);
    }
}
