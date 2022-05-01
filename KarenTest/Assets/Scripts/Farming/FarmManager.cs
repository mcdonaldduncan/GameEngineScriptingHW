using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager
{
    public List<Monster> farmMonsters;
    public MonsterStorage monsterStorage;
    public PlotHandler plotHandler;

    public FarmManager(int _plotsize, MonsterPlantPlot[] _monsterPlantPlots, FoodPlantPlot[] _foodPlantPlots)
    {
        farmMonsters = new List<Monster>();
        monsterStorage = new MonsterStorage();
        plotHandler = new PlotHandler(4, _monsterPlantPlots, _foodPlantPlots);
    }

    public void PrintMonsterList()
    {
        foreach(Monster m in farmMonsters)
        {
            Debug.Log(m.battleStats.Name);
        }
    }
}
