using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonPlantFactory
{
    private static MonPlantFactory instance;
    public static MonPlantFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MonPlantFactory();
            }

            return instance;
        }
    }

    public Dictionary<string, MonsterPlant> monPlantDictionary;

    public MonPlantFactory()
    {
        monPlantDictionary = new Dictionary<string, MonsterPlant>();
        FillDictionary();
    }

    void FillDictionary()
    {
        monPlantDictionary.Add("SunflowerLion", new MonsterPlant("SunflowerLion"));
        monPlantDictionary.Add("Advodoggo", new MonsterPlant("Advodoggo"));
        monPlantDictionary.Add("Strawbunny", new MonsterPlant("Strawbunny"));
        monPlantDictionary.Add("Raccorn", new MonsterPlant("Raccorn"));
    }

    public MonsterPlant GetMonPlant(string value)
    {
        MonsterPlant mp = null;

        if (monPlantDictionary.ContainsKey(value))
        {
            mp = monPlantDictionary[value];
        }
        else
        {
            Debug.Log($"{value} is not a monster plant");
        }

        return mp;
    }
}
