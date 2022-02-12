using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script that controls the physics-based
/// movement between the two paddles
/// </summary>
public class MoveSphere : MonoBehaviour
{
    /// <summary>
    /// Default speed to apply as force to the sphere
    /// </summary>
    public float speed = 20f;

    /// <summary>
    /// The default amount to add or subtract from speed whenever
    /// the user indicates that they want to increase or decrease 
    /// the speed.
    /// </summary>
    public float increment = .1f;

    /// <summary>
    /// The minimum velocity that the ball must travel in order
    /// to cause a bounce on the opposite paddle.
    /// </summary>
    public float minVelocity = 15f;

    /// <summary>
    /// The maximum velocity that the we will allow the ball 
    /// to travel in this simulation
    /// </summary>
    public float maxVelocity = 30000f;

    /// <summary>
    /// The string value used to compare tags in the OnCollision method
    /// </summary>
    private const string CollisionTag = "Paddle";

    /// <summary>
    /// A reference to the RigidBody of the sphere which will be 
    /// used in the FixedUpdate
    /// </summary>
    private Rigidbody rb = null;

    /// <summary>
    /// A bool that is set to true when the AddForce method should be used
    /// within the Fixed Update.
    /// This is true when the sphere collides with a paddle and then once 
    /// "bounced" by AddForce in Fixed Update is set to false.
    /// Think of this like a light switch.
    /// </summary>
    private bool MoveNow = true;

    /// <summary>
    /// Used to hold the next speed value which occurs after the users
    /// hits the [ or ] keys to increase or decrease the speed.
    /// Has public access due to TDD
    /// </summary>
    public float nextSpeed = 0;

    /// <summary>
    /// DO NOT MODIFY
    /// Save the reference to the rigid body here.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// DO NOT MODIFY
    /// All physics movements with rigid bodies should occur in 
    /// the FixedUpdate rather than the Update method
    /// </summary>
    private void FixedUpdate()
    {
        // If MoveNow is true then calculate a forward velocity
        // and apply it as a force to the rigid body.
        // Reset MoveNow to false after the AddForce is applied
        if (MoveNow)
        {
            Vector3 nextVelocity = Vector3.forward * speed;
            rb.AddForce(nextVelocity, ForceMode.Impulse);
            MoveNow = false;
        }
    }

    /// <summary>
    /// DO NOT MODIFY
    /// Update still is the method to use for handling Input.
    /// The Update looks for the keys [ or ] and
    /// assigns the Direction.Up or Direction.Down to the 
    /// Direction d value.
    /// If one of these directions were chosen then 
    /// call the AdjustSpeed method to get the 
    /// nextSpeed which is used in code called by 
    /// OnCollision
    /// </summary>
    void Update()
    {
        // Read for the [ or ] keys and initialize d to Up or Down
        Direction d = Direction.None;
        if (Input.GetKeyDown(KeyCode.LeftBracket))
            d = Direction.Up;
        else if (Input.GetKeyDown(KeyCode.RightBracket))
            d = Direction.Down;

        // If not No Direction (Up or Down) then 
        // call Adjust Speed with the requested Direction 
        // and the speed
        if (d != Direction.None)
        {
            AdjustSpeed(d, speed);
            Debug.Log($"Speed change from {speed} to {nextSpeed}");
        }
    }

    /// <summary>
    /// DO NOT MODIFY
    /// STUDY this section especially.
    /// This method is called when the user wants to 
    /// increase or decrease the speed of the sphere.
    /// The method initializes the field nextSpeed.
    /// There are three useful methods of the Unity Math library (Mathf)
    /// that are used here. 
    /// You will be expected to understand these methods and use them
    /// in the future.
    /// Mathf.Abs is to get the absolute value of a float or an integer.
    /// Mathf.Abs(1) == 1, Mathf.Abs(-1) == 1
    /// Mathf.Clamp returns the first parameter value unless it exceeds the 
    /// lower or higher boundary. If it exceeds on the low end then the 
    /// second parameter is the return value from Clamp. If it exceeds on the 
    /// high end then the third parameter is the return value.
    /// </summary>
    /// <param name="d">The requested direction</param>
    /// <param name="currentSpeed">A copy of the speed field</param>

    public void AdjustSpeed(Direction d, float currentSpeed)
    {
        // Get the absolute speed of the currentSpeed
        float absSpeed = Mathf.Abs(currentSpeed);

        // Either add the increment field value if the direction is Up,
        // or subtract if the direction is Down.
        nextSpeed = 0;
        if (d == Direction.Up)
            nextSpeed = absSpeed + increment;
        else if (d == Direction.Down)
            nextSpeed = absSpeed - increment;

        // Modify nextSpeed if it exceeds the min and max boundary values.
        nextSpeed = Mathf.Clamp(nextSpeed, minVelocity, maxVelocity);

        // If the currentSpeed, which has not been changed, has a negative
        // sign then return negative next speed.
        // Otherwise return positive next speed.
        if (Mathf.Sign(currentSpeed) == -1)
            nextSpeed = -nextSpeed;

    }

    /// <summary>
    /// DO NOT MODIFY 
    /// Study this code.
    /// If the sphere has collided with a Paddle then call the 
    /// ChangeDirectionBasedOnSpeed method. 
    /// You will change that method. 
    /// </summary>
    /// <param name="collision">The object that the sphere has collied with. 
    /// Do not assume that the collision object is a paddle. It could
    /// be the floor or any other object in the scene that has a collider.</param>
    private void OnCollisionEnter(Collision collision)
    {
        // Filter goes here: If we have collided with something(s)
        // that we are not interested in then leave the method.
        if (collision.gameObject.name == "Floor")
            return;

        // Check to see that the collision occurred with a game object that has 
        // a tag that matches the value of Collision Tag.
        // In this example that value is "Paddle".
        // We use a variable to make the method more flexible in the future.
        // If this statement is true then call ChangeDirectionBasedOnSpeed 
        // which is what you will work on.
        if (collision.gameObject.CompareTag(CollisionTag))
            ChangeDirectionBasedOnSpeed();


    }

    /// <summary>
    /// YOU WILL MAKE CHANGES to this Method!
    /// Things you need to do:
    /// <list>
    /// <item>If nextSpeed is not zero then assign the 
    /// nextSpeed value to the speed field 
    /// (By assign we mean that in the example below 
    /// a  variable x is the assigned the value of y 
    /// which has a value of 2.
    /// After the assignment x will also have the value of 2.
    /// int y = 2;
    /// int x = y;
    ///</item>
    ///<item>Change the MoveNow field to true</item>
    ///<item>Reverse the speed field. By reverse we mean if speed is positive then speed is now negative and
    ///if speed is negative then speed is now positive.
    ///Example:
    ///int x = 1;
    ///x = -x; 
    /// Now x is -1.
    /// Do it again.
    /// x = -x
    /// x is 1 again.
    /// </item>
    /// <item>Assign the value of zero to nextSpeed</item>
    /// </list>
    /// </summary>
    public void ChangeDirectionBasedOnSpeed()
    {
        // If nextSpeed is not zero then assign the value of nextSpeed to speed
        // YOUR WORK GOES HERE
        if (nextSpeed != 0)
        {
            speed = nextSpeed;
        }
        // Change MoveNow so that the code in FixedUpdate will run
        // AND HERE
        MoveNow = true;
        // Reverse the Speed
        // AND HERE
        speed = -speed;
        // Reinitialize nextSpeed to zero
        // AND HERE
        nextSpeed = 0;
    }

  



}