
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Utility;


public class AnswerDetection : MonoBehaviour
{
    /// <summary>
    /// Keep track of the current move 1 - 12
    /// </summary>
    [Header("The number of the current move")]
    [SerializeField] public int MoveNumber;

    /// <summary>
    /// The top-level GameObject for Rows so that
    /// Answer Detection can get all of the rows in 
    /// one inspector parameter
    /// </summary>
    [Header("Initialize with the Rows Parent Game Object")]
    [SerializeField] GameObject PlayerRows;

    /// <summary>
    /// The top-level GameObject for HintGrids so that
    /// Answer Detection can get all of the HintGrid items in 
    /// one inspector parameter
    /// </summary>
    [Header("Initialize with the HintGrids Parent Game Object")]
    [SerializeField] GameObject HintGrids;

    /// <summary>
    /// Specialized non-MonoBehaviour class 
    /// used to store all of the moves, hints,
    /// and which determines if the player has won.
    /// Note: This is in the Assets.Scripts namespace
    /// </summary>
    ScoreHistory scoreHistory;

    //[SerializeField] 
    GameObject[] currentRow;
    
    [SerializeField] GameObject[] answerKey;
    [SerializeField] GameObject[] pins;
    
    // [SerializeField] 
    GameObject hintGrid;
    [SerializeField] bool randomize;

    List<GameObject> sortedPins = new List<GameObject>();
    
    List<GameObject> randomizedPins = new List<GameObject>();

    
    /// <summary>
    /// Initialize a new instance of scoreHistory,
    /// pass the PlayerRows and HintGrids to 
    /// the constructor and call the initialization.
    /// </summary>
    void Start()
    {
        scoreHistory = new ScoreHistory(PlayerRows, HintGrids);
        GetGrids(MoveNumber);
    }

    private void GetGrids(int moveNumber)
    {
        if (scoreHistory != null)
        {
            scoreHistory.CurrentMove = moveNumber;
            currentRow = scoreHistory.GetCurrentPlayerRow();
            hintGrid = scoreHistory.GetCurrentHintGrid();
        }
    }

    /// <summary>
    /// Move to the next row in the game provided that 
    /// the player is not already on the last row
    /// </summary>
    void MoveToNextRow()
    {
        if (MoveNumber < PlayerRows.transform.childCount)
        {
            MoveNumber++;
            GetGrids(MoveNumber);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Evaluate();
            MoveToNextRow();
        }
    }

    private void Evaluate()
    {
        Material[] answerMats = new Material[answerKey.Length];
        for (int i = 0; i < answerKey.Length; i++)
        {
            Material temp = answerKey[i].GetComponent<MeshRenderer>().material;
            answerMats[i] = temp;
        }

        Material[] currentMats = new Material[currentRow.Length];
        for (int i = 0; i < currentRow.Length; i++)
        {
            Material temp = currentRow[i].GetComponent<MeshRenderer>().material;
            currentMats[i] = temp;
        }
        Report(answerMats, currentMats);
    }

    void Report(Material[] answerMats, Material[] currentMats)
    {
        // Clear lists so that new objects can be instantiated, remove when using multiple lists
        randomizedPins.Clear();
        sortedPins.Clear();

        int[] answerValues = new int[currentMats.Length];
        List<Material> compMats = answerMats.ToList();
        List<Color> colorAnswers = new List<Color>();

        foreach (var item in compMats)
        {
            colorAnswers.Add(item.color);
        }

        for (int i = 0; i < currentMats.Length; i++)
        {
            if (currentMats[i].color == answerMats[i].color)
            {
                answerValues[i] = 1;
                InstantiatePin(0, hintGrid.transform.GetChild(i).transform);
            }
            else if (colorAnswers.Contains(currentMats[i].color))
            {
                answerValues[i] = 0;
                InstantiatePin(1, hintGrid.transform.GetChild(i).transform);
            }
            else
            {
                answerValues[i] = -1;
                InstantiatePin(2, hintGrid.transform.GetChild(i).transform);
            }
            Debug.Log(answerValues[i]);
           

        }
        scoreHistory.SaveHistory(answerValues);

        if (randomize)
        {
            RandomizePins();
        }
    }

    void InstantiatePin(int index, Transform transform)
    {
        GameObject pin = Instantiate(pins[index]);
        pin.transform.position = transform.position;
        sortedPins.Add(pin);
    }

    void RandomizePins()
    {
        randomizedPins = sortedPins.OrderBy(x => random.Next()).ToList();

        for (int i = 0; i < randomizedPins.Count; i++)
        {
            randomizedPins[i].transform.position = hintGrid.transform.GetChild(i).transform.position;
        }
    }

    void AdvanceIndex()
    {

    }
}
