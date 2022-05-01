using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMenuUI : MonoBehaviour
{
    public Text monster1name;
    public Text monster1stats;
    public Text monster2name;
    public Text monster2stats;
    public Text monster3name;
    public Text monster3stats;

    public GameObject uiMenu;
    public GameObject feedMenu;

    public MonsterFeedUI monsterFeedUI;

    bool isVisible;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
        isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isVisible)
            {
                uiMenu.SetActive(false);
                Player.Instance.canMove = true;
                isVisible = false;
            } else
            {
                uiMenu.SetActive(true);
                Player.Instance.canMove = false;
                isVisible = true;
            }
        }
    }

    void UpdateText()
    {
        Debug.Log(Player.Instance.playerInventory.PrintMonsterName(0));

        monster1name.text = Player.Instance.playerInventory.PrintMonsterName(0);
        monster2name.text = Player.Instance.playerInventory.PrintMonsterName(1);
        monster3name.text = Player.Instance.playerInventory.PrintMonsterName(2);

        monster1stats.text = Player.Instance.playerInventory.PrintMonsterInfo(0);
        monster2stats.text = Player.Instance.playerInventory.PrintMonsterInfo(1);
        monster3stats.text = Player.Instance.playerInventory.PrintMonsterInfo(2);
    }

    public void SelectMonster1()
    {
        monsterFeedUI.monsterSelected = Player.Instance.playerInventory.ReturnMonster(0);
        uiMenu.SetActive(false);
        feedMenu.SetActive(true);
        monsterFeedUI.UpdateStatsText();
    }

    public void SelectMonster2()
    {
        monsterFeedUI.monsterSelected = Player.Instance.playerInventory.ReturnMonster(1);
        uiMenu.SetActive(false);
        feedMenu.SetActive(true);
        monsterFeedUI.UpdateStatsText();
    }

    public void SelectMonster3()
    {
        monsterFeedUI.monsterSelected = Player.Instance.playerInventory.ReturnMonster(2);
        uiMenu.SetActive(false);
        feedMenu.SetActive(true);
        monsterFeedUI.UpdateStatsText();
    }

    public void Back()
    {
        feedMenu.SetActive(false);
        uiMenu.SetActive(true);
        isVisible = true;
    }
}
