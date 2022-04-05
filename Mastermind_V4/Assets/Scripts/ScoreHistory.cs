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
        private GameObject playerRows;
        private GameObject hintGrids;
        public int CurrentMove { get; set; } 

        public void SaveHistory(int[] tanswers)
        {
            for (int i = 0; i < tanswers.Length; i++)
            {
                this.answers[CurrentMove, i] = tanswers[i];
            }
        }

        public int[,] answers { get; set; }

        /// <summary>
        /// Initialize the constructor with the top-level objects
        /// relating to the Player Rows and the Hint Grid
        /// </summary>
        /// <param name="playerRows"></param>
        /// <param name="hintGrids"></param>
        public ScoreHistory(GameObject playerRows, GameObject hintGrids)
        {
            CurrentMove = 0;
            this.playerRows = playerRows;
            this.hintGrids = hintGrids;
            this.answers = new int[6,4];
        }

        /// <summary>
        /// Retrieve the Player Row for the current move
        /// </summary>
        /// <returns>An array of Game Objects</returns>
        public GameObject[] GetCurrentPlayerRow ()
        {
            Transform t = playerRows.transform;
            if (CurrentMove <= t.childCount)
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
            if (CurrentMove <= t.childCount)
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
