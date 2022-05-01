using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonster : MonoBehaviour
{
    int moveIndex;

    public BattleStats battleStats;
    public Move MoveChosen;
    public string statsKey;

    private void Start()
    {
        this.battleStats = BattleStatsFactory.Instance.GetStats(statsKey);
    }

    public void DetermineMove()
    {
        moveIndex = Random.Range(0, 4);
    }

    public Move UseMove()
    {
        DetermineMove();

        Move moveToUse = new Move();

        if (moveIndex == 0)
        {
            moveToUse = this.battleStats.MonsterMoveSet.Move1;
        }

        if (moveIndex == 1)
        {
            moveToUse = this.battleStats.MonsterMoveSet.Move2;
        }

        if (moveIndex == 2)
        {
            moveToUse = this.battleStats.MonsterMoveSet.Move3;
        }

        if (moveIndex == 3)
        {
            moveToUse = this.battleStats.MonsterMoveSet.Move4;
        }

        return moveToUse;
    }
}
