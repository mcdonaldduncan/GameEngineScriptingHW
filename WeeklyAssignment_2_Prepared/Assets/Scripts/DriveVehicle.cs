using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;   
using UnityEngine.UI;


/// <summary>
/// Class to handle Driving the vehicle which includes:
/// Acceleration, Deceleration
/// Pause/Resume 
/// Updating the current position and vehicle speed
/// </summary>
public class DriveVehicle : MonoBehaviour
{
    // DO NOT MAKE ANY CHANGES UNTIL LINE 185

    /// <summary>
    /// Speed of the vehicle
    /// </summary>
    [Header("The vehicle speed")]
    public float Speed = 1f;


    /// <summary>
    /// A specific Road "segment" which is used as a location to
    /// do things like stop the vehicle.
    /// </summary>
    [Header("The road segment where the camera will stop following")]
    public Transform StoppingPoint;

    /// <summary>
    /// Empty Game Object that contains the array of road "segments"
    /// as child objects of the transform of Road.
    /// </summary>
    [Header("The Unity developer needs to reference the Road")]
    public GameObject Road;

    /// <summary>
    /// A bool state to see debugging messages in the Unity Console
    /// </summary>
    [Header("Set to true to see the debugging messages")]
    public bool ShowDebuggingMessages;

    /// <summary>
    /// The data type, TMP_Text, refers to a Text type for 
    /// Text Mesh Pro. This parameter is used to show the 
    /// position of the vehicle.
    /// </summary>
    [Header("Reference to the Text_KiloMarker in the UI ")]
    public Text KmText;


    /// <summary>
    /// The data type, TMP_Text, refers to a Text type for 
    /// Text Mesh Pro.
    /// Shows the current speed
    /// </summary>
    [Header("Reference to the Text_Speed in the UI ")]
    public Text SpeedText;


    /// <summary>
    /// A bool state to determine if the Camera has past its
    /// stopping point.
    /// </summary>
    private bool HasReachedStoppingPoint;


    /// <summary>
    /// A bool state to determine if the vehicle is pausing.
    /// </summary>
    private bool PauseState = false;


    
    /// <summary>
    /// DO NOT CHANGE
    /// </summary>
    void Start()
    {
        // Useful check to determine that the designer has assigned part of the road to the StoppingPoint parameter.
        Debug.Assert(StoppingPoint != null, "The Stopping point for the car must be initialized to a part of the Road");
        Debug.Assert(Road != null, "The car must be aware of the entire Road!");
        //Debug.Assert(KmText != null, "The KmText must refer to a Text Mesh Pro Text object!");
        UpdateKmPositionDisplay();
        UpdateSpeedDisplay();
    }




    /// <summary>
    /// DO NOT CHANGE
    /// DO NOT MODIFY THE CODE within the Update Block
    /// Only make changes in the SetPauseState,UpdateSpeed, MoveVehicle, 
    /// and CheckForStop methods.
    /// The update will look for the following key strokes:
    /// Space Bar (to Pause / Resume moving)
    /// Up/Down Arrows or w/s keys ( to Accelerate, Decelerate the Speed)
    /// </summary>
    void Update()
    {
        float CurrentDeltaTime = Time.deltaTime;

        // If the vehicle has moved past the stopping point then stop
        // moving the vehicle
        if (HasReachedStoppingPoint)
            return;

        // Update the Kilometer UI
        UpdateKmPositionDisplay();

        // True if user clicked the space bar
        bool UserClickedSpaceBar = Input.GetKeyDown(KeyCode.Space);
        // The user did not click the space bar so 
        // skip the rest of the method
        if (UserClickedSpaceBar)
            PauseState = SetPauseState(PauseState);

        // Get the vertical axis value to pass to a speed up/slow down method
        // Uses the Up/Down keys
        float speedDiff = Input.GetAxis("Vertical");
        // Update the Speed value using a static method
        Speed = UpdateSpeed(Speed, speedDiff);
        UpdateSpeedDisplay();

        // If the vehicle is NOT Paused 
        // ! is the symbol for NOT
        // !True is False, !False is True
        if (!PauseState)
        {
            transform.position = CalculateNextPosition(transform.position, CurrentDeltaTime);
            HasReachedStoppingPoint = CheckForStop(transform.position, StoppingPoint.position);
        }
    }


    /// <summary>
    /// DO NOT CHANGE
    /// Update the Kilometer UI to indicate the position of the car along the road
    /// </summary>
    private void UpdateKmPositionDisplay()
    {
        // Report where we are in the road
        float currentZ = transform.position.z;
        bool fnd = false;
        Transform roadSegments = Road.transform;
        for (int i = 0; i < roadSegments.childCount && !fnd; i++)
        {
            if (currentZ >= roadSegments.GetChild(i).transform.position.z)
            {
                KmText.text = $"Km: {i}";
                Debug.Log($"Just passed Road Segment {i}");
                fnd = true;
            }
        }
    }

    /// <summary>
    /// DO NOT CHANGE
    /// Update the Speed UI to show the current speed of the vehicle
    /// </summary>
    private void UpdateSpeedDisplay()
    {
        double truncatedSpeedForDisplay = System.Math.Round(Speed, 1);
        SpeedText.text = $"Speed: {truncatedSpeedForDisplay}";
    }


    // ALL CHANGES ARE MADE AFTER THIS POINT
    // DO NOT Change the method signatures 
    // public <return type> <methodname> (<Parameters>)
    //

    /// <summary>
    /// Problem 1
    /// Using a static method to create a new speed
    /// This method will not have access to the Speed field.
    /// Only use the parameters to calculate the a new speed
    /// and then return that new speed.
    /// However this speed can never go below zero! The 
    /// vehicle never goes into reverse!
    /// </summary>
    /// <param name="currentSpeed">The current speed of the vehicle</param>
    /// <param name="speedDiff">The adjustment amount which can be positive or negative</param>
    /// <returns>The sum of the current speed and the speedDiff</returns>
    public static float UpdateSpeed(float currentSpeed, float speedDiff)
    {
        // Replace the -1 on the next line with the calculation
        float newSpeed = currentSpeed + speedDiff;
        // Make sure that newSpeed is not less than zero
        // Code goes here for a few lines
        if (newSpeed < 0)
        {
            newSpeed = 0;
        }
        return newSpeed;
    }


    /// <summary>
    /// Problem 2
    /// Method acts as a toggle between pausing the vehicle or not.
    /// Set Pause to true if Pause had been false,
    /// or set Pause to false if Pause had been true.
    /// </summary>
    /// <param name="currentPauseState">Current value of PauseState</param>
    public static bool SetPauseState(bool currentPauseState)
    {
        // Flip or reverse the pause state
        bool nextPauseState = !currentPauseState;

        return nextPauseState;
    }

    /// <summary>
    /// Problem 3
    /// Using a static method to create a new speed
    /// This method will not have access to the StoppingPoint field.
    /// Determine if the vehicle has moved past the stopping position
    /// Keep in mind that the z-values of the Road get smaller as we move from one end to the other.
    /// </summary>
    /// <param name="currentPosition">The current position of the transform</param>
    public static bool CheckForStop(Vector3 currentPosition, Vector3 stoppingPosition)
    {
        // Replace the following with several lines that determine if the currentPosition parameter
        // is the same or less than the stoppingPosition parameter
        if (currentPosition.z <= stoppingPosition.z)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }


    /// <summary>
    /// Problem 4
    /// Calculate the amount to move as a Vector3 named moveVec.
    /// Calculate the next position from the currentPosition and moveVec
    /// Remember that the vehicle is moving on the z-axis from a larger 
    /// number to a smaller number (in fact moving from a positive number 
    /// to a negative number)
    /// IMPORTANT: 
    /// 1. The convenience vector that moves backward on the z-vector is Vector3.back.
    /// 2. You need to scale the backward adjustment by the Speed, which is a class variable,
    /// and the diffTime which is a parameter.
    /// 
    /// Return the next position value
    /// NOTE: Product is the result of multiplication
    /// EXAMPLE: x is the product of a, b 
    /// x = a * b * c
    /// </summary>
    /// <param name="currentPosition">A copy of the position of the transform</param>
    /// <param name="diffTime">A time value</param>
    public Vector3 CalculateNextPosition(Vector3 currentPosition, float diffTime)
    {
        
        Vector3 nextPosition = currentPosition + Vector3.back * Speed * diffTime;

        // Several lines to calculate the nextPosition based on the formula used in the
        // comments above.

        if (ShowDebuggingMessages)
        {
            Debug.Log($"Vehicle Speed:{Speed},Time.deltaTime:{diffTime}");
            Debug.Log($"Vehicle Vector3.forward * Speed * Time.deltaTime");
            Debug.Log($"Vehicle Updated Position = {nextPosition}");

        }
        return nextPosition;
    }

}
