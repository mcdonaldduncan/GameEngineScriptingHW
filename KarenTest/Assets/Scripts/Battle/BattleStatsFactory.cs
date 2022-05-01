using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatsFactory
{
    private static BattleStatsFactory instance;
    public static BattleStatsFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BattleStatsFactory();
            }

            return instance;
        }
    }

    public Dictionary<string, BattleStats> statsDictionary;

    public BattleStatsFactory()
    {
        statsDictionary = new Dictionary<string, BattleStats>();
        FillDictionary();
    }

    void FillDictionary()
    {
        statsDictionary.Add("SunflowerLion", new BattleStats(BattleTypeDatabase.FlowerType, "Default", "SunflowerLion"));
        statsDictionary.Add("Advodoggo", new BattleStats(BattleTypeDatabase.FruitType, "Default", "Advodoggo"));
        statsDictionary.Add("Strawbunny", new BattleStats(BattleTypeDatabase.FruitType, "Default", "Strawbunny"));
        statsDictionary.Add("Raccorn", new BattleStats(BattleTypeDatabase.VeggieType, "Default", "Raccorn"));
    }

    public BattleStats GetStats(string value)
    {
        BattleStats bs = null;

        if (statsDictionary.ContainsKey(value))
        {
            bs = statsDictionary[value];
        }
        else
        {
            Debug.Log($"{value} is not a monster");
        }

        return bs;
    }
}
