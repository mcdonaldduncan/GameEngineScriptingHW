using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster
{
    public BattleStats battleStats;
    public MonsterMovement monMonvement;
    public Sprite monsterSprite;

    public Monster(string _statsKey)
    {
        this.battleStats = BattleStatsFactory.Instance.GetStats(_statsKey);
    }

    public Monster(string _statsKey, string _spritePath)
    {
        this.battleStats = BattleStatsFactory.Instance.GetStats(_statsKey);
        this.monsterSprite = Resources.Load<Sprite>(_spritePath);
    }

    public void FeedMonster(Food f)
    {
        if (Player.Instance.playerInventory.GetAmountOfFood(f.Name) > 0)
        {
            f.Feed(this.battleStats.MonsterStats);
            Player.Instance.playerInventory.RemoveOneFood(f.Name);
        }
    }

    public void SetMaxHP()
    {
        this.battleStats.MonsterStats.CurrentHP = this.battleStats.MonsterStats.MaxHP;
    }
}
