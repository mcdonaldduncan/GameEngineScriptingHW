using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStorageUI : MonoBehaviour
{
    public Text monster1Name;
    public Text monster2Name;
    public Text monster3Name;

    public Text monster1Stats;
    public Text monster2Stats;
    public Text monster3Stats;

    public Text monster1NameStorage;
    public Text monster2NameStorage;
    public Text monster3NameStorage;
    public Text monster4NameStorage;

    public Text monster1StatsStorage;
    public Text monster2StatsStorage;
    public Text monster3StatsStorage;
    public Text monster4StatsStorage;

    public Text resultsText;

    public GameObject storageUI;

    bool triggerActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggerActive = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggerActive = false;
    }

    private void Update()
    {
        if (triggerActive && Input.GetKeyDown(KeyCode.Space))
        {
            ShowSelf();
        }
    }

    public void UpdateUI()
    {
        UpdatePlayerTeamText();
        UpdatePlayerStorageText();
    }

    public void UpdatePlayerTeamText()
    {
        monster1Name.text = Player.Instance.playerInventory.PrintMonsterName(0);
        monster2Name.text = Player.Instance.playerInventory.PrintMonsterName(1);
        monster3Name.text = Player.Instance.playerInventory.PrintMonsterName(2);

        monster1Stats.text = Player.Instance.playerInventory.PrintMonsterInfo(0);
        monster2Stats.text = Player.Instance.playerInventory.PrintMonsterInfo(1);
        monster3Stats.text = Player.Instance.playerInventory.PrintMonsterInfo(2);
    }

    public void UpdatePlayerStorageText()
    {
        monster1NameStorage.text = GameManager.SharedInstance.farmManager.monsterStorage.PrintMonsterName(0);
        monster2NameStorage.text = GameManager.SharedInstance.farmManager.monsterStorage.PrintMonsterName(1);
        monster3NameStorage.text = GameManager.SharedInstance.farmManager.monsterStorage.PrintMonsterName(2);
        monster4NameStorage.text = GameManager.SharedInstance.farmManager.monsterStorage.PrintMonsterName(3);

        monster1StatsStorage.text = GameManager.SharedInstance.farmManager.monsterStorage.PrintMonsterInfo(0);
        monster2StatsStorage.text = GameManager.SharedInstance.farmManager.monsterStorage.PrintMonsterInfo(1);
        monster3StatsStorage.text = GameManager.SharedInstance.farmManager.monsterStorage.PrintMonsterInfo(2);
        monster4StatsStorage.text = GameManager.SharedInstance.farmManager.monsterStorage.PrintMonsterInfo(3);
    }

    public void TeamOnetoStorage()
    {
        GameManager.SharedInstance.farmManager.monsterStorage.AddMonsterToStorage(Player.Instance.playerInventory.ReturnMonster(0));
        Player.Instance.playerInventory.RemoveMonster(0);
        UpdateUI();
    }

    public void TeamTwotoStorage()
    {
        GameManager.SharedInstance.farmManager.monsterStorage.AddMonsterToStorage(Player.Instance.playerInventory.ReturnMonster(1));
        Player.Instance.playerInventory.RemoveMonster(1);
        UpdateUI();
    }

    public void TeamThreetoStorage()
    {
        GameManager.SharedInstance.farmManager.monsterStorage.AddMonsterToStorage(Player.Instance.playerInventory.ReturnMonster(2));
        Player.Instance.playerInventory.RemoveMonster(2);
        UpdateUI();
    }

    public void StorageOnetoTeam()
    {
        resultsText.text = GameManager.SharedInstance.farmManager.monsterStorage.AddMonsterToPlayerTeam(GameManager.SharedInstance.farmManager.monsterStorage.ReturnMonsterStorage(0));

        if (resultsText.text != "You need to clear up a space on your team first")
        {
            GameManager.SharedInstance.farmManager.monsterStorage.RemoveMonsterFromStorage(0);
        }

        UpdateUI();
    }

    public void StorageTwotoTeam()
    {
        resultsText.text = GameManager.SharedInstance.farmManager.monsterStorage.AddMonsterToPlayerTeam(GameManager.SharedInstance.farmManager.monsterStorage.ReturnMonsterStorage(1));

        if (resultsText.text != "You need to clear up a space on your team first")
        {
            GameManager.SharedInstance.farmManager.monsterStorage.RemoveMonsterFromStorage(1);
        }

        UpdateUI();
    }

    public void StorageThreetoTeam()
    {
        resultsText.text = GameManager.SharedInstance.farmManager.monsterStorage.AddMonsterToPlayerTeam(GameManager.SharedInstance.farmManager.monsterStorage.ReturnMonsterStorage(2));

        if (resultsText.text != "You need to clear up a space on your team first")
        {
            GameManager.SharedInstance.farmManager.monsterStorage.RemoveMonsterFromStorage(2);
        }

        UpdateUI();
    }

    public void StorageFourtoTeam()
    {
        resultsText.text = GameManager.SharedInstance.farmManager.monsterStorage.AddMonsterToPlayerTeam(GameManager.SharedInstance.farmManager.monsterStorage.ReturnMonsterStorage(3));

        if (resultsText.text != "You need to clear up a space on your team first")
        {
            GameManager.SharedInstance.farmManager.monsterStorage.RemoveMonsterFromStorage(3);
        }

        UpdateUI();
    }

    public void ShowSelf()
    {
        Player.Instance.canMove = false;
        storageUI.gameObject.SetActive(true);
        UpdateUI();
    }

    public void HideSelf()
    {
        Player.Instance.canMove = true;
        storageUI.gameObject.SetActive(false);
    }
}
