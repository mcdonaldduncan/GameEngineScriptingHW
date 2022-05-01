using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Farming,
    Battling,
    Paused
}

public class GameStateManager
{
    public GameStates currentGameState;
    public List<GameObject> battleGameObjects;
    public List<GameObject> farmingGameObjects;

    public GameStateManager()
    {
        this.currentGameState = GameStates.Farming;
        battleGameObjects = new List<GameObject>();
        farmingGameObjects = new List<GameObject>();
    }

    public void SetFarmingState()
    {
        this.currentGameState = GameStates.Farming;
        ActivateObjects(farmingGameObjects);
        DeactivateObjects(battleGameObjects);
    }

    public void SetBattleState()
    {
        this.currentGameState = GameStates.Battling;
        ActivateObjects(battleGameObjects);
        DeactivateObjects(farmingGameObjects);
    }

    void ActivateObjects(List<GameObject> objects)
    {
        foreach(GameObject g in objects) 
        {
            g.SetActive(true);
        }
    }

    void DeactivateObjects(List<GameObject> objects)
    {
        foreach(GameObject g in objects)
        {
            g.SetActive(false);
        }
    }

    void DeactivateAll()
    {
        foreach (GameObject g in battleGameObjects)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in farmingGameObjects)
        {
            g.SetActive(false);
        }
    }
}
