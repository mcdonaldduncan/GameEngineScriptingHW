using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterFeedUI : MonoBehaviour
{
    public Monster monsterSelected;
    public Text statsText;
    public Text nameText;

    public Text hpBerryText;
    public Text atkBerryText;
    public Text defBerryText;
    public Text spdBerryText;

    public Image monsterSprite;

    private void Start()
    {
        
    }

    public void FeedHPBerry()
    {
        monsterSelected.FeedMonster(FoodFactory.Instance.GetFood("HP Berry"));
        monsterSelected.SetMaxHP();
        UpdateStatsText();
    }

    public void FeedATKBerry()
    {
        monsterSelected.FeedMonster(FoodFactory.Instance.GetFood("ATK Berry"));
        UpdateStatsText();
    }

    public void FeedDEFBerry()
    {
        monsterSelected.FeedMonster(FoodFactory.Instance.GetFood("DEF Berry"));
        UpdateStatsText();
    }

    public void FeedSPDBerry()
    {
        monsterSelected.FeedMonster(FoodFactory.Instance.GetFood("SPD Berry"));
        UpdateStatsText();
    }

    public void UpdateStatsText()
    {
        nameText.text = monsterSelected.battleStats.Name;
        statsText.text = monsterSelected.battleStats.Describe();

        monsterSprite.sprite = monsterSelected.monsterSprite;

        hpBerryText.text = $"You have {Player.Instance.playerInventory.GetAmountOfFood("HP Berry")}";
        atkBerryText.text = $"You have {Player.Instance.playerInventory.GetAmountOfFood("ATK Berry")}";
        defBerryText.text = $"You have {Player.Instance.playerInventory.GetAmountOfFood("DEF Berry")}";
        spdBerryText.text = $"You have {Player.Instance.playerInventory.GetAmountOfFood("SPD Berry")}";
    }
}
