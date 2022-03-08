using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeMoverWithBarriers : MonoBehaviour
{
    /// <summary>
    /// The planes that the maze is constructed from
    /// </summary>
    public GameObject[] planes;


    

    /// <summary>
    /// User can adjust the speed that is used in the weighted speed calculation
    /// </summary>
    public float Speed = 1f;

    /// <summary>
    /// TextMesh Pro control used to display the current room
    /// </summary>
    public TMP_Text positionText;



    /// <summary>
    /// An array of planes that holds the 
    /// boundaries of each plane
    /// </summary>
    private Bounds[] planesBoundaries;

    /// <summary>
    /// A reference to the bounds of the game object which is used in 
    /// several places.
    /// </summary>
    private Bounds cubeBounds;

    /// <summary>
    /// true if the camera is looking back.
    /// This will be used to modify the input keys since
    /// if true then the camera is looking at the subject 
    /// and so left-right need to be reversed.
    /// </summary>
    public bool IsCameraLookingBack = false;


    /// <summary>
    /// The various objects marked as barriers
    /// </summary>
    private GameObject[] barriers;

    private Bounds[] barrierBounds;

    /// <summary>
    /// 1. Determine if the camera is looking back
    /// 2. Initialize the boundaries of each plane of the maze
    /// 3. Find the current location.
    /// 4. Get all of the objects in the hierarchy tagged with Barrier and save them to the barriers array
    /// </summary>
    void Start()
    {
        // The camera is looking back if the rotation of the camera around
        // it's y-axis is negative.


        cubeBounds = transform.GetComponent<MeshRenderer>().bounds;
        // Need the height of the cube to expand the boundaries
        // of each plane in to the space above the plane
        float heightOfCube = cubeBounds.size.y;



        // Create an array of Bounds that has the same size as planes
        planesBoundaries = new Bounds[planes.Length];

        // Initialize each bounds element with the bounds of each plane
        for (int i = 0; i < planes.Length; i++)
        {
            planesBoundaries[i] = planes[i].GetComponent<MeshRenderer>().bounds;

            // Expand the height (y) of the boundary since a plane is flat.
            planesBoundaries[i].Encapsulate(new Vector3(planesBoundaries[i].center.x,
                    heightOfCube, planesBoundaries[i].center.z));
            Debug.LogFormat("{0}:{1}", i, planesBoundaries[i].extents);
        }

        // FindGameObjectsWithTag finds each game object that has a tag that 
        // matches the parameter
        barriers = GameObject.FindGameObjectsWithTag("Barrier");

        // Initialize a barrierBounds array just like the planesBoundaries
        barrierBounds = new Bounds[barriers.Length];

        // Initialize each bounds element with the bounds of each plane.
        // Since the barriers are not planes and have a height then there is no 
        // need to expand the height of the barrier. That can be done by the designer.
        for (int i = 0; i < barriers.Length; i++)
        {
            barrierBounds[i] = barriers[i].GetComponent<MeshRenderer>().bounds;
        }



        Vector3 eulerAngles = Camera.main.transform.rotation.eulerAngles;
        Debug.LogFormat("eulerAngles.y = {0}", eulerAngles.y);
        IsCameraLookingBack = eulerAngles.y >= 180 && eulerAngles.y <= 360;

        FindCurrentLocation(this.gameObject);
    }

    /// <summary>
    /// Use boundary testing to determine which 
    /// part of the maze the game object occupies
    /// </summary>
    /// <param name="gameObject">The game object that we need to test</param>
    private void FindCurrentLocation(GameObject gameObject)
    {
        int planeFound = -1;
        string planeName = String.Empty;

        // Look through all of the planes to find any plane that surrounds the game object
        // If the game object goes outside of the maze then no plane will be found.
        for (int i = 0; i < planesBoundaries.Length && planeFound == -1; i++)
        {
            // The Contains method of the Bounds class returns true if a 
            // vector is within a boundary
            if (planesBoundaries[i].Contains(gameObject.transform.position))
            {
                planeFound = i;
                planeName = planes[i].gameObject.name;
            }
        }
        // If a plane containing the cube has been found then update the UI
        if (planeFound != -1)
        {
            Debug.Log($"Cube is on plane {planeName}");
            // Update the UI
            positionText.text = planeName;
        }
    }

    /// <summary>
    /// Test for the current location of the game object (FindCurrentLocation)
    /// everytim ethe game object moves on the XZ plane.
    /// </summary>
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // Move the game object if there is any movement.
        // Flip the sign of the vertical value
        // if the camera is looking back at the maze.
        if (vertical != 0 || horizontal != 0)
        {
            if (IsCameraLookingBack)
            {
                vertical = -vertical;
            }

            float weight = Time.deltaTime * Speed;
            float weightedVert = vertical * weight;
            float weightedHoriz = horizontal * weight;

            // The arguments are switched since we want the ASDW or arrow keys to 
            // move the character left, right with AD (left, right) and 
            // forward and back with SW (down, up)

            Vector3 nextVec = new Vector3(weightedVert, 0, weightedHoriz);
            Vector3 projectedPosition = this.transform.position + nextVec;

            if (!HitABarrier(projectedPosition))
            {
                this.transform.Translate(nextVec);
                FindCurrentLocation(this.gameObject);
            }


        }


    }

    /// <summary>
    /// Determine if the projectedPosition will hit an existing barrier.
    /// This code is very similar to StayInMaze
    /// </summary>
    /// <param name="projectedPosition"></param>
    /// <returns></returns>
    private bool HitABarrier(Vector3 projectedPosition)
    {
        int barrierFound = -1;

        // Look through all of the planes to find any plane that surrounds the game object
        // If the game object goes outside of the maze then no plane will be found.
        for (int i = 0; i < barrierBounds.Length && barrierFound == -1; i++)
        {
            // The Contains method of the Bounds class returns true if a 
            // vector is within a boundary
            if (barrierBounds[i].Contains(projectedPosition))
            {
                barrierFound = i;
            }
        }
        // What is happening here? Why is not finding a barrier returning true?
        if (barrierFound == -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
