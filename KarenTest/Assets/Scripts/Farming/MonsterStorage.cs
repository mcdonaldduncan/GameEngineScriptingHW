using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStorage
{
    private List<Monster> monstersInStorage;

    public MonsterStorage()
    {
        monstersInStorage = new List<Monster>();
    }

    public void AddMonsterToStorage(Monster m)
    {
        monstersInStorage.Add(m);
    }

    public void RemoveMonsterFromStorage(int index)
    {
        if (index < monstersInStorage.Count)
        {
            monstersInStorage.Remove(monstersInStorage[index]);
        }
    }

    public string ListMonstersInStorage()
    {
        string list = "";

        foreach (Monster m in monstersInStorage)
        {
            list += $"{m.battleStats.Name}\n";
        }

        return list;
    }

    public string PrintMonsterInfo(int index)
    {
        if (index < monstersInStorage.Count)
        {
            return monstersInStorage[index].battleStats.Describe();
        }

        return "";
    }

    public string PrintMonsterName(int index)
    {
        if (index < monstersInStorage.Count)
        {
            return monstersInStorage[index].battleStats.Name;
        }

        return "";
    }

    public Monster ReturnMonsterStorage(int index)
    {
        return monstersInStorage[index];
    }

    public string AddMonsterToPlayerTeam(Monster m)
    {
        if (Player.Instance.playerInventory.CanAddMonster())
        {
            Player.Instance.playerInventory.AddMonster(m);
            return $"{m.battleStats.Name} added to team";
        }

        return "You need to clear up a space on your team first";
    }
}
