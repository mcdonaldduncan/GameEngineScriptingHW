using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Number - integer (int) 1, 3, 5  float 1.1f, 2.5f, 3.8f
    // Text - string, char
    // True/False - Boolean (bool)

    //int myNumber;
    //float newNumber = 5;

    //string myName = "Duncan";

    //bool isTrue = false;

    public int wheels = 4;
    string playerName = "Carbot";
    int speed = 70;
    bool powerUp = false;

    int coins = 20;

    // Start is called before the first frame update
    void Start()
    {
        /*
        Debug.Log("I am Carbot - Bringer of Justice");

        Debug.Log(playerName + " has " + wheels + " wheels");

        wheels = wheels + 2;

        Debug.Log($"{playerName} has {wheels} wheels");

        wheels += 2;

        Debug.Log($"{playerName} got an upgrade! They now have {wheels} wheels.");

        wheels = 50;

        Debug.Log($"{playerName} got an SUPER upgrade! They now have {wheels} wheels.");
        */


        //if (wheels > 4)
        //{
        //    Debug.Log(playerName + " has " + wheels + " wheels");
        //}


        //for (int i = 1; i <= coins; i++)
        //{
        //    Debug.Log($"{i} coins have been generated");
        //}

        int value = 1;

        while (value <= coins)
        {
            Debug.Log($"{value} coins have been generated");
            value++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
