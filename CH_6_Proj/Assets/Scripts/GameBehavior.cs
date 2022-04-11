using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    public Button WinButton;

    public int MaxItems = 4;
    
    public Text HealthText;
    public Text ItemText;
    public Text ProgressText;
    
    void Start()
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;
    }
    private int _itemsCollected = 0;
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            // 5
            ItemText.text = "Items Collected: " + Items;
            // 6
            if (_itemsCollected >= MaxItems)
            {
                ProgressText.text = "You've found all the items!";
                WinButton.gameObject.SetActive(true);

                Time.timeScale = 0f;
            }
            else
            {
                ProgressText.text = "Item found, only " +
                (MaxItems - _itemsCollected) + " more to go!";
            }
        }
    }
    private int _playerHP = 10;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            // 7
            HealthText.text = "Player Health: " + HP;
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);

        Time.timeScale = 1f;
    }
}
