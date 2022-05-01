using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler
{
    public Monster monsterBattler;

    public Battler()
    {
        monsterBattler = MonsterFactory.Instance.GetMon("SunflowerLion");
    }
}
