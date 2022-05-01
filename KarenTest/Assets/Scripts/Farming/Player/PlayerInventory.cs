using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory
{
    private List<Monster> playerMonsters;
    private List<Food> playerFood;
    public int teamSize;

    public int maxTeamSize;

    public PlayerInventory()
    {
        playerMonsters = new List<Monster>();
        playerFood = new List<Food>();
        maxTeamSize = 3;

        AddMonster(MonsterFactory.Instance.GetMon("Advodoggo"));
        AddMonster(MonsterFactory.Instance.GetMon("SunflowerLion"));
        AddMonster(MonsterFactory.Instance.GetMon("Raccorn"));
    }

    // Later I will have to abstract this out to monsterinventory 

    public void AddMonster(Monster m)
    {
        this.playerMonsters.Add(m);
        teamSize++;
    }

    public Monster ReturnMonster(int index)
    {
        return playerMonsters[index];
    }

    public bool CanAddMonster()
    {
        return playerMonsters.Count + 1 <= maxTeamSize;
    }

    public void AddFood(Food f)
    {
        this.playerFood.Add(f);
    }

    public string PrintMonsterInfo(int index)
    {
        if (index < playerMonsters.Count)
        {
            return playerMonsters[index].battleStats.Describe();
        }

        return "";
    }

    public string PrintMonsterName(int index)
    {
        if (index < playerMonsters.Count)
        {
            return playerMonsters[index].battleStats.Name;
        }

        return "";
    }

    public void RemoveMonster(int index)
    {
        if (index < playerMonsters.Count)
        {
            playerMonsters.Remove(playerMonsters[index]);
            teamSize--;
        }
    }

    public Monster GetBattler(int index)
    {
        return playerMonsters[index];
    }

    public int GetAmountOfFood(string key)
    {
        int amount = 0;

        foreach(Food f in playerFood)
        {
            if (key == f.Name)
            {
                amount++;
            }
        }

        return amount;
    }

    public void RemoveOneFood(string key)
    {
        int index = 0;

        for (int i = 0; i < playerFood.Count; i++)
        {
            if (playerFood[i].Name == key)
            {
                index = i;
            }
        }

        playerFood.Remove(playerFood[index]);
    }
}
