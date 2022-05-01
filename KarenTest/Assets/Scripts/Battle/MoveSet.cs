using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet
{
    private Move move1;
    private Move move2;
    private Move move3;
    private Move move4;

    public Move Move1
    {
        get { return move1; }
    }
    public Move Move2
    {
        get { return move2; }
    }
    public Move Move3
    {
        get { return move3; }
    }
    public Move Move4
    {
        get { return move4; }
    }

    public MoveSet()
    {
        move1 = MoveFactory.Instance.GetMove("VeggieBite");
        move2 = MoveFactory.Instance.GetMove("FruitSlash");
        move3 = MoveFactory.Instance.GetMove("FlowerBite");
        move4 = MoveFactory.Instance.GetMove("FlowerSlash");
    }

    public MoveSet(string _move1key, string _move2key, string _move3key, string _move4key)
    {
        move1 = MoveFactory.Instance.GetMove(_move1key);
        move2 = MoveFactory.Instance.GetMove(_move2key);
        move3 = MoveFactory.Instance.GetMove(_move3key);
        move4 = MoveFactory.Instance.GetMove(_move4key);
    }
}
