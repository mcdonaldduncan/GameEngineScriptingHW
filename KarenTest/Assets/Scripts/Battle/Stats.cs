using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    private int maxHP;
    private int currentHP;

    private int attack;
    private int defense;
    private int speed;

    public int MaxHP { get { return maxHP; } set { maxHP = value; } }
    public int CurrentHP
    {
        get { return currentHP; }
        set { if (CurrentHP <= 0) { currentHP = 0; } else { currentHP = value; } }
    }
    public int Attack { get { return attack; } set { attack = value; } }
    public int Defense { get { return defense; } set { defense = value; } }
    public int Speed { get { return speed; } set { speed = value; } }

    public Stats(int _maxHP, int _currentHP, int _attack, int _defense, int _speed)
    {
        this.maxHP = _maxHP;
        this.currentHP = _currentHP;
        this.attack = _attack;
        this.defense = _defense;
        this.speed = _speed;
    }
}
