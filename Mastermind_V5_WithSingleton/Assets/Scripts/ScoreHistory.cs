using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// ScoreHistory will be used to 
    /// retrieve the current Row and HintGrid.
    /// It will store the answer grids for the Hint Grids
    /// </summary>
    class ScoreHistory
    {
        /// <summary>
        /// Reference to the playerRows top-level object
        /// </summary>
        private GameObject playerRows;

        /// <summary>
        /// Reference to the hintGrids top-level object
        /// </summary>
        private GameObject hintGrids;


        /// <summary>
        /// A private property to simplify access to the CurrentMove property
        /// of the gameManager. Properties are rarely private but in this case
        /// it makes sense.
        /// </summary>
        private int CurrentMove => gameManager.CurrentMove;

        /// <summary>
        /// Cache a reference to the gameManager for easier access
        /// </summary>
        private GameManager gameManager;


        /// <summary>
        /// Used to save the history of moves for 
        /// storage to a file.
        /// </summary>
        /// <param name="tanswers"></param>
        public void SaveHistory(int[] tanswers)
        {
            for (int i = 0; i < tanswers.Length; i++)
            {
                this.answers[CurrentMove, i] = tanswers[i];
            }
        }

        /// <summary>
        /// 2-dimensional array of the evaluation of the answers.
        /// For each move there is a row in this array. Each row
        /// has as many columns as there are positions in a row in 
        /// the PlayerRows.
        /// </summary>
        public int[,] answers { get; set; }
        /// <summary>
        /// Initialize the constructor with the top-level objects
        /// relating to the Player Rows and the Hint Grid.
        /// Get the number of potential answers from the playerRows
        /// The GameManager 
        /// </summary>
        /// <param name="playerRows"></param>
        /// <param name="hintGrids"></param>
        public ScoreHistory(GameObject playerRows, GameObject hintGrids)
        {
            gameManager = GameManager.Instance;
            this.playerRows = playerRows;
            this.hintGrids = hintGrids;

            int numRows = gameManager.MaxMoves;
            int numOfAnswersPerRow = hintGrids.transform.GetChild(0).childCount;
            this.answers = new int[numRows, numOfAnswersPerRow];
        }

        /// <summary>
        /// Retrieve the Player Row for the current move
        /// </summary>
        /// <returns>An array of Game Objects</returns>
        public GameObject[] GetCurrentPlayerRow ()
        {
            Transform t = playerRows.transform;
            if (CurrentMove < t.childCount)
            {
                Transform childTransform = t.transform.GetChild(CurrentMove);
                int arrayLen = childTransform.childCount;
                GameObject[] currentRowGameObjects = new GameObject[arrayLen];

                for (int i = 0; i < arrayLen; i++)
                {
                    currentRowGameObjects[i] = childTransform.GetChild(i).gameObject;
                }
                return currentRowGameObjects;
            }
            else
            {
                return null;
            }
                
        }

        /// <summary>
        /// Retrieve the Hint Grid for the current move
        /// </summary>
        /// <returns>A Game Object</returns>
        public GameObject GetCurrentHintGrid()
        {
            Transform t = hintGrids.transform;
            if (CurrentMove < t.childCount)
            {
                Transform childTransform = t.transform.GetChild(CurrentMove);
                return childTransform.gameObject;
            }
            else
            {
                return null;
            }

        }


    }
}
