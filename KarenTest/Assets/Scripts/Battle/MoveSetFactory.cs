using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetFactory
{
    private static MoveSetFactory instance;
    public static MoveSetFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MoveSetFactory();
            }

            return instance;
        }
    }

    public Dictionary<string, MoveSet> movesetDictionary;

    public MoveSetFactory()
    {
        movesetDictionary = new Dictionary<string, MoveSet>();
    }

    public MoveSet GetMoveSet(string value)
    {
        MoveSet ms = null;

        if (movesetDictionary.ContainsKey(value))
        {
            ms = movesetDictionary[value];
        }
        else
        {
            switch (value)
            {
                case ("Default"):
                    ms = new MoveSet();
                    break;
            }
        }

        return ms;
    }
}
