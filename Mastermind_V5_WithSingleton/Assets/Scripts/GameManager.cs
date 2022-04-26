using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Here is a sample singleton
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// A reference to the Rows Game Object which is the parent of all 
    /// the individual Row objects.
    /// </summary>
    public GameObject Rows;

    /// <summary>
    /// An index used to select a child game object of the 
    /// Rows GameObject
    /// </summary>
    public int CurrentMove;

    /// <summary>
    /// Set this to the number of child game objects in the Rows GameObject
    /// </summary>
    public int MaxMoves { get; private set; }

    // All the methods that we need for the GameManager
    AnswerDetection answerDetection;

    static private GameManager _instance;

    static public GameManager Instance
    {
        get { return _instance; }
    }

    /// <summary>
    /// Initialize the single instance of Game Manager 
    /// and also set the MaxMoves class variable, which is the 
    /// number of rows from the Rows GameObject and is used
    /// to control the advance of the CurrentMove counter
    /// </summary>
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            MaxMoves = Rows.transform.childCount;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }
    public bool CanAdvance() => CurrentMove < MaxMoves;

    private void Start()
    {
        answerDetection = GameObject.Find("AnswerDetector").GetComponent<AnswerDetection>();
        answerDetection.winGame = false;
    }

    public void AdvanceCurrentMove()
    {
        if (CanAdvance())
            CurrentMove++;
        
    }

     
    
}
