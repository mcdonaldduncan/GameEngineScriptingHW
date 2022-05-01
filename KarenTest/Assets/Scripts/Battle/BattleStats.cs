using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStats
{
    private MoveSet monsterMoveSet;
    private BattleType monsterBattleType;
    private Stats monsterStats;

    public MoveSet MonsterMoveSet
    {
        get { return monsterMoveSet; }
    }

    string name;

    public BattleType MonsterBattleType { get { return monsterBattleType; } }

    public Stats MonsterStats { get { return monsterStats; } }

    public string Name { get { return name; } set { name = value; }  }

    public BattleStats()
    {
        this.monsterMoveSet = new MoveSet();
        this.monsterBattleType = BattleTypeDatabase.FlowerType;
        this.monsterStats = new Stats(20, 20, 2, 2, 2);
    }

    public BattleStats(BattleType _monType, string _moveSetKey, string _monsterName)
    {
        this.monsterBattleType = _monType;
        this.monsterMoveSet = MoveSetFactory.Instance.GetMoveSet(_moveSetKey);
        this.monsterStats = new Stats(20, 20, 2, 2, 2);
        this.name = _monsterName;
    }

    // This is in here because it can have access to the battle stats of its own monster
    public int DetermineDamage(Move move, BattleStats otherMonster)
    {
        int basedamage = move.Power * monsterStats.Attack;
        int damage = basedamage;

        // Check if the type of the move is good or bad against the type of the other mon
        if (move.MoveType == otherMonster.monsterBattleType.TypeWeakAgainst)
        {
            damage *= 2;
        } 
        
        if (move.MoveType == otherMonster.monsterBattleType.TypeGoodAgainst)
        {
            damage /= 2;
        }

        return damage;
    }

    public string Describe()
    {
        return $"HP: {MonsterStats.MaxHP} Attack: {MonsterStats.Attack} Defense: {MonsterStats.Defense} Speed: {MonsterStats.Speed}";
    }
}
