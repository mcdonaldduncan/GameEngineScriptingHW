using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public Text move1ButtonText;
    public Text move2ButtonText;
    public Text move3ButtonText;
    public Text move4ButtonText;

    public Text playerMonName;
    public Text enemyMonName;
    public Text playerMonHP;
    public Text enemyMonHP;
    public Text playerMonType;
    public Text enemyMonType;

    public Text enemyMoveText;

    public Image playerSprite;
    public Image enemySprite;

    public BattleManager battleManager;
    public GameObject winScreen;

    public void ConnectBattleManager(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        SetupUI();
    }

    public void SetupUI()
    {
        winScreen.gameObject.SetActive(false);

        this.playerMonName.text = battleManager.playerBattler.monsterBattler.battleStats.Name;
        this.enemyMonName.text = $"Enemy {battleManager.enemyMonster.battleStats.Name}";

        this.playerMonHP.text = $"{battleManager.playerBattler.monsterBattler.battleStats.MonsterStats.CurrentHP} / {battleManager.playerBattler.monsterBattler.battleStats.MonsterStats.MaxHP}";
        this.enemyMonHP.text = $"{battleManager.enemyMonster.battleStats.MonsterStats.CurrentHP} / {battleManager.enemyMonster.battleStats.MonsterStats.MaxHP}";

        this.playerMonType.text = $"{battleManager.playerBattler.monsterBattler.battleStats.MonsterBattleType.Name} Type";
        this.enemyMonType.text = $"{battleManager.enemyMonster.battleStats.MonsterBattleType.Name} Type";

        Debug.Log(battleManager.playerBattler.monsterBattler.monsterSprite);
        this.playerSprite.sprite = battleManager.playerBattler.monsterBattler.monsterSprite;

        SetUpButtons();
    }

    public void SetUpButtons()
    {
        move1ButtonText.text = battleManager.playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move1.Name;
        move2ButtonText.text = battleManager.playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move2.Name;
        move3ButtonText.text = battleManager.playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move3.Name;
        move4ButtonText.text = battleManager.playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move4.Name;
    }

    public void UseMove(int damage, int newHP, string moveName)
    {
        this.enemyMonHP.text = $"{newHP} / {battleManager.enemyMonster.battleStats.MonsterStats.MaxHP}";
    }

    public void UseEnemyMove(int damage, int newHP, string moveName)
    {
        this.playerMonHP.text = $"{newHP} / {battleManager.enemyMonster.battleStats.MonsterStats.MaxHP}";
        this.enemyMoveText.text = $"Enemy {battleManager.enemyMonster.battleStats.Name} used {moveName}!";
    }

    public void ResetHP()
    {
        this.battleManager.ResetHPForTesting();
        this.playerMonHP.text = $"{battleManager.playerBattler.monsterBattler.battleStats.MonsterStats.CurrentHP} / {battleManager.playerBattler.monsterBattler.battleStats.MonsterStats.MaxHP}";
        this.enemyMonHP.text = $"{battleManager.enemyMonster.battleStats.MonsterStats.CurrentHP} / {battleManager.enemyMonster.battleStats.MonsterStats.MaxHP}";
    }

    public void ShowWin()
    {
        winScreen.gameObject.SetActive(true);
    }

    public void HideWin()
    {
        winScreen.gameObject.SetActive(false);
        battleManager.ResetHPForTesting();
    }

    public void SwitchMon()
    {
        if (battleManager.playerMonIndex+1 < Player.Instance.playerInventory.teamSize)
        {
            battleManager.playerMonIndex++;
        } else
        {
            battleManager.playerMonIndex = 0;
        }

        battleManager.SetMonster();
    }
}
