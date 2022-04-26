
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Utility;


public class AnswerDetection : MonoBehaviour
{
    /// <summary>
    /// Reference to another script
    /// </summary>
    private GameManager gameManager;

    private ClickManager clickManager;

    public GameObject answers;

    public Text gameOverText;

    public bool winGame;
    /// <summary>
    /// A private property as a convenience to access the CurrentMove in the 
    /// gameManager.
    /// </summary>
    private int MoveNumber => gameManager == null ? -1 : gameManager.CurrentMove;

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

    public GameObject RowIndicator;

    /// <summary>
    /// Specialized non-MonoBehaviour class 
    /// used to store all of the moves, hints,
    /// and which determines if the player has won.
    /// Note: This is in the Assets.Scripts namespace
    /// </summary>
    private ScoreHistory scoreHistory;

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
    /// Save the distance of the Y-value between the first and second row vectors 
    /// assuming that all of the moves on the board are consistent.
    /// </summary>
    private float DistanceBetweenRows;
    
    void RandomizeAnswerKey()
    {
        List<Material> materials = clickManager.colorTilesMaterials.ToList();

        for (int i = 0; i < answerKey.Length; i++)
        {
            int random = UnityEngine.Random.Range(0, materials.Count);
            answerKey[i].GetComponent<MeshRenderer>().material = materials[random];
            materials.RemoveAt(random);
        }

    }
    
    /// <summary>
    /// Initialize a new instance of scoreHistory,
    /// pass the PlayerRows and HintGrids to 
    /// the constructor and call the initialization.
    /// </summary>
    void Start()
    {
        winGame = false;

        gameManager = GameManager.Instance;
        // Reference to the script component attached to this class
        //gameManager = GetComponent<GameManager>();
        //if (gameManager == null)
        //    Debug.LogError("The GameManager is not attached to the AnswerDetector!");

        clickManager = GameObject.Find("ClickDetector").GetComponent<ClickManager>();

        scoreHistory = new ScoreHistory(PlayerRows, HintGrids);

        answers.SetActive(false);

        GetGrids();
        SetDistanceBetweenRows();
        RandomizeAnswerKey();
    }

    /// <summary>
    /// Determine the Y-Value distance between the first child object of each of the 
    /// first two rows.
    /// Determine the difference in the Y-value and save this to the 
    /// variable DistanceBetweenRows which is used later to move the row indicator.
    /// </summary>
    private void SetDistanceBetweenRows()
    {
        if (PlayerRows.transform.childCount > 1)
        {
            Vector3 rowOneVec = PlayerRows.transform.GetChild(0).GetChild(0).position;
            Vector3 rowTwoVec = PlayerRows.transform.GetChild(1).GetChild(0).position;
            DistanceBetweenRows = rowTwoVec.y - rowOneVec.y;
        }
    }

    private void GetGrids()
    {
        if (scoreHistory != null)
        {
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
        if (gameManager != null && gameManager.CanAdvance())
        {
            gameManager.AdvanceCurrentMove();
            GetGrids();
            RowIndicator.transform.Translate(0,DistanceBetweenRows,0);
        }

    }

    void Update()
    {
        SetText();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Evaluate();
            MoveToNextRow();
        }
    }

    private void Evaluate()
    {
        if (!gameManager.CanAdvance())
            return;

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
        if (winGame)
            return;

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
          //  Debug.Log(answerValues[i]);
           

        }
        scoreHistory.SaveHistory(answerValues);

        if (randomize)
        {
            RandomizePins();
        }

        int answerSum = 0;

        for (int i = 0; i < answerValues.Length; i++)
        {
            answerSum += answerValues[i];
        }

        if (answerSum == 4)
        {
            winGame = true;
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

    void SetText()
    {
        if (winGame)
        {
            gameOverText.text = "You Win!";
            RowIndicator.SetActive(false);
            answers.SetActive(true);
        }
        else if (!gameManager.CanAdvance())
        {
            gameOverText.text = "Game Over!";
            RowIndicator.SetActive(false);
            answers.SetActive(true);
        }
        else
        {
            gameOverText.text = $"{gameManager.MaxMoves - gameManager.CurrentMove} Moves Remaining";
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Mastermind");
        gameManager.CurrentMove = 0;
        RowIndicator.SetActive(true);
        RandomizeAnswerKey();

    }

    void AdvanceIndex()
    {

    }
}
