using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleType
{
    public string Name;
    private BattleType typeGoodAgainst;
    private BattleType typeWeakAgainst;

    public BattleType TypeGoodAgainst { get { return typeGoodAgainst; } }
    public BattleType TypeWeakAgainst { get { return typeWeakAgainst; } }

    public BattleType(string name)
    {
        this.Name = name;
    }

    public BattleType(string name, BattleType _typeGoodAgainst, BattleType _typeWeakAgainst)
    {
        this.Name = name;
        this.typeGoodAgainst = _typeGoodAgainst;
        this.typeWeakAgainst = _typeWeakAgainst;
    }
}
