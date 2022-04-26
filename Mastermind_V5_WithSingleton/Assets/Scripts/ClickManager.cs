using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ClickManager : MonoBehaviour
{
    private Transform[] gameTiles;
    private Transform[] colorTiles;
    [NonSerialized] public Material[] colorTilesMaterials;
    private Material currentMaterial = null;

    /// <summary>
    /// The top-level GameObject for Rows so that
    /// Answer Detection can get all of the rows in 
    /// one inspector parameter
    /// </summary>
    [Header("Initialize with the Rows Parent Game Object")]
    [SerializeField] GameObject playerRows;


    AnswerDetection answerDetection;
    /// <summary>
    /// Reference to another script
    /// </summary>
    private GameManager gameManager;

    private Dictionary<int, GameObject> RowByMove;


    // Start is called before the first frame update
    void Start()
    {
        RowByMove = new Dictionary<int, GameObject>();
        InitializeRowsByMove();

        gameTiles = GameObject.FindGameObjectsWithTag("GameTile").
            Select(go => go.GetComponent<Transform>()).ToArray();

        gameTiles.ToList().ForEach(gt => Debug.Log($"Game Tile {gt.name}"));

        colorTiles = GameObject.FindGameObjectsWithTag("ColorTile").Select(go => go.GetComponent<Transform>()).ToArray();
        gameTiles.ToList().ForEach(gt => Debug.Log($"Color Tile {gt.name}"));

        colorTilesMaterials = colorTiles.ToList().Select(t => t.GetComponent<MeshRenderer>().material).ToArray();

        // Find the AnswerDetector GameObject, get it's script component and then get the reference
        // to the GameManager
        gameManager = GameManager.Instance;

        answerDetection = GameObject.Find("AnswerDetector").GetComponent<AnswerDetection>();
    }

    

    private void InitializeRowsByMove()
    {
        int position = 0;
        foreach (Transform row in playerRows.transform)
        {
            this.RowByMove.Add(position++, row.gameObject);
        }
    }


    private void ChangeRowState(bool state)
    {
        GameObject currentRow = RowByMove[gameManager.CurrentMove];

    }

    // Update is called once per frame
    void Update()
    {
        if (answerDetection.winGame)
            return;
        if (!gameManager.CanAdvance())
            return;
        
        //Debug.LogFormat("Current Move is {0}", gameManager.CurrentMove);
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        Vector3 mp = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mp);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (gameTiles.Contains(hit.transform))
            {
                GameObject currentRow = null;
                if (this.RowByMove.ContainsKey(gameManager.CurrentMove))
                    currentRow =this.RowByMove[gameManager.CurrentMove];

                bool canClick = false;
                foreach (Transform item in currentRow.transform)
                {
                    if (hit.transform == item)
                    {
                        canClick = true;
                        break;
                    }
                }

               // Debug.Log($"Clicked on Game Tile {hit.transform.name} at {hit.transform.position}");
                if (canClick && currentMaterial != null)
                    hit.transform.GetComponent<MeshRenderer>().material = currentMaterial;
            }
            else if (colorTiles.Contains(hit.transform))
            {
              //  Debug.Log($"Clicked on Color Tile {hit.transform.name} at {hit.transform.position}");
                int pos = Array.IndexOf(colorTiles, hit.transform);
                currentMaterial = colorTilesMaterials[pos];
            }
        }
        //else
        //{
        //    Debug.Log($"Clicked at {mp} without hitting anything");
        //}
    }
}
