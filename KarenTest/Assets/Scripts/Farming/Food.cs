using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food
{
    int value;

    public int Value { get { return value; } }
    // 0 is HP, 1 is Power, 2 is Def, 3 is Speed
    public int Stat;
    public string Name;
    public Sprite FoodSprite;

    public Food(string _name, int _stat, int _value, string _spritepath)
    {
        this.Name = _name;
        this.Stat = _stat;
        this.value = _value;
        this.FoodSprite = Resources.Load<Sprite>(_spritepath);
    }

    public void Feed(Stats monStats)
    {
        if (Stat == 0)
        {
            monStats.MaxHP += value;
        }

        if (Stat == 1)
        {
            monStats.Attack += value;
        }

        if (Stat == 2)
        {
            monStats.Defense += value;
        }

        if (Stat == 3)
        {
            monStats.Speed += value;
        }
    }
}
