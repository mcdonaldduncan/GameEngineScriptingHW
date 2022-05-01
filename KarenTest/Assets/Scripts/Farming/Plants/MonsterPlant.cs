using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlant : Plant
{
    public Monster monster;
    public string plantMonKey;

    public MonsterPlant(string key)
    {
        this.plantMonKey = key;
        this.monster = MonsterFactory.Instance.GetMon(key);
        this.plantEvo = new PlantEvo();
    }
}
