using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int sceneID;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneID);
    }

    public void LoadFarmingScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene(1);
    }
}
