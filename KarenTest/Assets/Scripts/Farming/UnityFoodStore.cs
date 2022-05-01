using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityFoodStore : MonoBehaviour
{
    FoodPlantStore foodPlantStore;
    public Text resultText;
    public GameObject Menu;

    bool triggerActive = false;
    bool beingShown = false;

    void Start()
    {
        foodPlantStore = new FoodPlantStore();
    }

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
        if (triggerActive && !beingShown && Input.GetKeyDown(KeyCode.Space))
        {
            ShowSelf();
        }
    }

    // Connect to UI

    public void BuyHPBerry()
    {
        resultText.text = foodPlantStore.SellFoodPlant("HP Berry");
    }

    public void BuyATKBerry()
    {
        resultText.text = foodPlantStore.SellFoodPlant("ATK Berry");
    } 

    public void BuyDEFBerry() 
    {
        resultText.text = foodPlantStore.SellFoodPlant("DEF Berry");
    }

    public void BuySPDBerry()
    {
        resultText.text = foodPlantStore.SellFoodPlant("SPD Berry");
    }

    public void ShowSelf()
    {
        Player.Instance.canMove = false;
        Menu.SetActive(true);
    }

    public void HideSelf()
    {
        Player.Instance.canMove = true;
        Menu.SetActive(false);
    }
}
