using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    private string name;
    private int power;
    private BattleType moveType;

    public string Name
    {
        get { return name; }
    }
    public int Power { get { return power; } }
    public BattleType MoveType { get { return moveType; } }

    public Move()
    {

    }

    public Move(string _name, int _power, BattleType _movetype)
    {
        this.name = _name;
        this.power = _power;
        this.moveType = _movetype;
    }
}
