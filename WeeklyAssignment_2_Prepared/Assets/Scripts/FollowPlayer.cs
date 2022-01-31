using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle Driving the vehicle which includes:
/// Acceleration, Deceleration
/// Pause/Resume 
/// Updating the current position and vehicle speed
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    // DO NOT MAKE ANY CHANGES UNTIL LINE 142

    [Header("The Game Object or Subject that the camera will follow")]
    public Transform subject;

    [Header("Amount of time in milliseconds to delay following the subject")]
    public float delayBeforeStart;

    [Header("Use the same or slightly less speed as the Car to maintain a consistent distance ")]
    public float Speed;

    [Header("The road segment where the camera will stop following")]
    public Transform StoppingPoint;

    /// <summary>
    /// True to see debugging messages in the Unity Console
    /// </summary>
    [Header("Set to true to see the debugging messages")]
    public bool ShowDebuggingMessages;


    /// <summary>
    /// The sum of the InitialTime and the delayBeforeStart
    /// </summary>
    private float CameraStartTime;

    /// <summary>
    /// CurrentTime is incremented by Time.deltaTime
    /// each time through Update
    /// </summary>
    private float CurrentTime;

    /// <summary>
    /// A flag to determine if the Camera is moving.
    /// Initially this is set to false
    /// </summary>
    private bool IsCameraMoving;

    /// <summary>
    /// A flag to determine if the Camera has past its
    /// stopping point.
    /// </summary>
    private bool HasReachedStoppingPoint;


    /// <summary>
    /// Keep track of the position of the Subject (Vehicle)
    /// to determine if it has moved since the last update.
    /// </summary>
    public Vector3 lastSubjectPosition;

    /// <summary>
    /// DO NOT CHANGE
    /// Intialize the CameraStartTime.
    /// Time values are in seconds so 
    /// the delayBeforeStart parameter needs to 
    /// be divided by 1000.
    /// </summary>
    void Start()
    {
        // Useful check to determine that the designer has assigned part of the road to the StoppingPoint parameter.
        Debug.Assert(StoppingPoint != null, "The Stopping point for the camera must be initialized to a part of the Road");
        CameraStartTime = delayBeforeStart / 1000;
        lastSubjectPosition = subject.position;
    }

    /// <summary>
    /// DO NOT CHANGE
    /// With each update increment the current time.
    /// Start following the subject once the Current Time 
    /// excees the CameraStartTime
    /// </summary>
    void Update()
    {
        // If the subject is paused then do not move
        // the camera on this update.
        if (IsSubjectPaused(subject.position, lastSubjectPosition))
            return;

        UpdateLastSubjectPosition(subject.position);

        // If the camera has reached the stopping point
        // then exit the update as there is nothing to do.
        if (HasReachedStoppingPoint)
            return;

        float CurrentDeltaTime = Time.deltaTime;
        CurrentTime += CurrentDeltaTime;
        if (!CheckIfPastCameraDelay(CurrentTime))
            return;

        transform.position = CalculateNextPosition(transform.position, CurrentDeltaTime);

        HasReachedStoppingPoint = CheckForStop(transform.position, StoppingPoint.position);
    }





    /// <summary>
    /// DO NOT CHANGE
    /// Determine if the Delay period is past which "frees"
    /// the camera to follow the subject.
    /// </summary>
    /// <param name="CurrentTime">The total elapsed time running this script</param>
    /// <returns>True if the time delay has expired</returns>
    private bool CheckIfPastCameraDelay(float CurrentTime)
    {
        if (ShowDebuggingMessages)
            Debug.Log($"Current Time = {CurrentTime}, Camera Start Time = {CameraStartTime}");

        // Should the camera start chasing the car
        if (!IsCameraMoving)
        {
            if (CurrentTime >= CameraStartTime)
            {
                IsCameraMoving = true;
            }
        }

        return IsCameraMoving;
    }

    /// <summary>
    /// Problem 1
    /// NOTE: This will be exactly the same as CheckForStop in DriveVehicle
    /// Determine if the camera has moved past the stopping point
    /// Keep in mind that the z-values of the Road get smaller as we move from one end to the other.
    /// </summary>
    /// <param name="currentPosition">The current position of the transform</param>
    public static bool CheckForStop(Vector3 currentPosition, Vector3 stoppingPosition)
    {
        // Replace the following with what you used in CheckForStop in DriveVehicle
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
    /// Problem 2
    /// NOTE: The answer for CalulateNextPosition in DriveVehice will work here without any change.
    /// </summary>
    /// <param name="currentPosition">A copy of the position of the transform</param>
    /// <param name="diffTime">A time value</param>
    public Vector3 CalculateNextPosition(Vector3 currentPosition, float diffTime)
    {
        // The current statement on line 163 keeps the camera at the starting position.
        // You need to replace the code in this method with the 
        // same code that you used in the CalculateNextPosition method of DriveVehicle
        //Vector3 nextPosition = lastSubjectPosition;
        Vector3 nextPosition = currentPosition + Vector3.back * Speed * diffTime;
        //currentPosition + Vector3.back * Speed * diffTime;
        return nextPosition;
    }

    /// <summary>
    /// Problem 3
    /// If the subject has moved then update the lastSubjectPosition 
    /// field to the value of the position parameter
    /// </summary>
    /// <param name="position">The current position of the subject</param>
    public void UpdateLastSubjectPosition(Vector3 position)
    {
        // Assign the value of the parameter to the lastSubjectPosition 
        if (subject.position.z != position.z)
        {
            lastSubjectPosition = position;
        }
    }

    /// <summary>
    /// Problem 4
    /// This is a static method so the method will not have 
    /// access to the class fields. 
    /// ONLY use the parameters to determine if the subject has 
    /// paused ie stopped moving.
    /// 
    /// Use Vector arithmetic to determine if there is a difference 
    /// in the movement. All movement is along the z axis.
    /// If the result of the arithmetic is zero then the subject is 
    /// not moving ie it has paused or stopped motion.
    /// HINT: You can also use the Vector3.Distance method to determine the distance between two vectors.
    /// </summary>
    /// <param name="position">The current position of the subject</param>
    /// <returns>True if the subject is not moving</returns>
    public static bool IsSubjectPaused(Vector3 position, Vector3 lastPosition)
    {
        // You need to change the value of diffZ to equal the difference between
        // position and lastPosition
        float diffZ = Vector3.Distance(position, lastPosition);
        // DO NOT CHANGE THE NEXT LINE
        return diffZ == 0f;
        
    }
}
