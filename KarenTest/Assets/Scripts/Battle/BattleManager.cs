using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Win,
    Loss
}

public class BattleManager : MonoBehaviour
{
    //public UnityMonster playerMonster;
    public EnemyMonster enemyMonster;

    public Battler playerBattler;
    //public Battler enemyBattler;

    public BattleUIManager battleUI;
    public BattleState battleState;

    public int playerMonIndex;

    private void Start()
    {
        
    }

    private void Awake()
    {
        playerMonIndex = 0;
        playerBattler = new Battler();
        this.battleState = BattleState.Start;
    }

    public void SetMonster()
    {
        if (playerMonIndex < Player.Instance.playerInventory.teamSize)
        {
            playerBattler.monsterBattler = Player.Instance.playerInventory.GetBattler(playerMonIndex);
        }
        SetupUI();
    }

    // Methods for player using moves via buttons in UI
    public void UseMoveOne()
    {
        if (playerBattler.monsterBattler.battleStats.MonsterStats.CurrentHP > 0)
        {
            int damage = playerBattler.monsterBattler.battleStats.DetermineDamage(playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move1, enemyMonster.battleStats);
            int newEnemyHP = ApplyDamage(damage, enemyMonster.battleStats);

            if (newEnemyHP < 0)
            {
                newEnemyHP = 0;
                battleUI.ShowWin();
            }

            battleUI.UseMove(damage, newEnemyHP, playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move1.Name);
            EnemyTurn();
        }
    }

    public void UseMoveTwo()
    {
        if (playerBattler.monsterBattler.battleStats.MonsterStats.CurrentHP > 0)
        {
            int damage = playerBattler.monsterBattler.battleStats.DetermineDamage(playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move2, enemyMonster.battleStats);
            int newEnemyHP = ApplyDamage(damage, enemyMonster.battleStats);

            if (newEnemyHP < 0)
            {
                newEnemyHP = 0;
                battleUI.ShowWin();
            }

            battleUI.UseMove(damage, newEnemyHP, playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move2.Name);
            EnemyTurn();
        }
    }

    public void UseMoveThree()
    {
        if (playerBattler.monsterBattler.battleStats.MonsterStats.CurrentHP > 0)
        {
            int damage = playerBattler.monsterBattler.battleStats.DetermineDamage(playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move3, enemyMonster.battleStats);
            int newEnemyHP = ApplyDamage(damage, enemyMonster.battleStats);

            if (newEnemyHP < 0)
            {
                newEnemyHP = 0;
                battleUI.ShowWin();
            }

            battleUI.UseMove(damage, newEnemyHP, playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move3.Name);
            EnemyTurn();
        }
    }

    public void UseMoveFour()
    {
        if (playerBattler.monsterBattler.battleStats.MonsterStats.CurrentHP > 0)
        {
            int damage = playerBattler.monsterBattler.battleStats.DetermineDamage(playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move4, enemyMonster.battleStats);
            int newEnemyHP = ApplyDamage(damage, enemyMonster.battleStats);

            if (newEnemyHP < 0)
            {
                newEnemyHP = 0;
                battleUI.ShowWin();
            }

            battleUI.UseMove(damage, newEnemyHP, playerBattler.monsterBattler.battleStats.MonsterMoveSet.Move4.Name);
            EnemyTurn();
        }
    }

    // Method to apply damage to other monster
    public int ApplyDamage(int damage, BattleStats otherMonster)
    {
        otherMonster.MonsterStats.CurrentHP -= damage;

        return otherMonster.MonsterStats.CurrentHP;
    }

    void EnemyTurn()
    {
        Move enemyMove = enemyMonster.UseMove();

        int damage = enemyMonster.battleStats.DetermineDamage(enemyMove, playerBattler.monsterBattler.battleStats);
        int newPlayerHP = ApplyDamage(damage, playerBattler.monsterBattler.battleStats);

        if (newPlayerHP < 0)
        {
            newPlayerHP = 0;
        }

        battleUI.UseEnemyMove(damage, newPlayerHP, enemyMove.Name);
    }

    void SetupUI()
    {
        battleUI.ConnectBattleManager(this);
    }

    public void ResetHPForTesting()
    {
        playerBattler.monsterBattler.battleStats.MonsterStats.CurrentHP = playerBattler.monsterBattler.battleStats.MonsterStats.MaxHP;
        enemyMonster.battleStats.MonsterStats.CurrentHP = enemyMonster.battleStats.MonsterStats.MaxHP;
        //SetupUI();
    }
}
