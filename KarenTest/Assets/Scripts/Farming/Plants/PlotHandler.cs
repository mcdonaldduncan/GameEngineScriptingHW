using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotHandler
{
    private MonsterPlantPlot[] monsterPlantPlots;
    private FoodPlantPlot[] foodPlantPlots;
    int plotSize;

    public PlotHandler()
    {
        plotSize = 4;
        monsterPlantPlots = new MonsterPlantPlot[plotSize];
        foodPlantPlots = new FoodPlantPlot[plotSize];
    }

    public PlotHandler(int _plotsize, MonsterPlantPlot[] _monsterPlantPlots, FoodPlantPlot[] _foodPlantPlots)
    {
        this.plotSize = _plotsize;
        this.monsterPlantPlots = _monsterPlantPlots;
        this.foodPlantPlots = _foodPlantPlots;
    }

    public bool CanBuyFoodSeed()
    {
        bool result = false;
        
        for (int i = 0; i < plotSize; i++)
        {
            if (foodPlantPlots[i].plotStatus == PlotStatus.Empty)
            {
                return true;
            }
        }

        return result;
    }

    public bool CanBuyMonSeed()
    {
        bool result = false;

        for (int i = 0; i < plotSize; i++)
        {
            if (monsterPlantPlots[i].plotStatus == PlotStatus.Empty)
            {
                return true;
            }
        }

        return result;
    }

    public void FillFirstFoodPlot(FoodPlant newPlant)
    {
        for (int i = 0; i < plotSize; i++)
        {
            if (foodPlantPlots[i].plotStatus == PlotStatus.Empty)
            {
                foodPlantPlots[i].SetFoodPlant(newPlant);
                return;
            }
        }
    }

    public void FillFirstMonsterPlot(MonsterPlant newPlant)
    {
        for (int i = 0; i < plotSize; i++)
        {
            if (monsterPlantPlots[i].plotStatus == PlotStatus.Empty)
            {
                monsterPlantPlots[i].SetMonsterPlant(newPlant);
                return;
            }
        }
    }
}
