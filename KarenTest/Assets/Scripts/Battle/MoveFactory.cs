using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFactory
{
    private static MoveFactory instance;
    public static MoveFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MoveFactory();
            }

            return instance;
        }
    }

    public Dictionary<string, Move> moveDictionary;

    public MoveFactory()
    {
        moveDictionary = new Dictionary<string, Move>();
        FillDictionary();
    }

    void FillDictionary()
    {
        moveDictionary.Add("VeggieBite", new Move("Veggie Bite", 4, BattleTypeDatabase.VeggieType));
        moveDictionary.Add("FruitSlash", new Move("Fruit Slash", 4, BattleTypeDatabase.FruitType));
        moveDictionary.Add("FlowerBite", new Move("Flower Bite", 4, BattleTypeDatabase.FlowerType));
        moveDictionary.Add("FlowerSlash", new Move("Flower Slash", 2, BattleTypeDatabase.FlowerType));
    }

    public Move GetMove(string value)
    {
        Move m = null;

        if (moveDictionary.ContainsKey(value))
        {
            m = moveDictionary[value];
        }
        else
        {
            Debug.Log($"{value} is not a monster");
        }

        return m;
    }
}
