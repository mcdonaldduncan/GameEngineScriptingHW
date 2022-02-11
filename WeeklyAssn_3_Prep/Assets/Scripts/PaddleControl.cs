using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// An enum, a typical computer language feature,
/// exists so that we can create symbolic constants.
/// Instead of thinking of Left as being 0 and Right as 1
/// the code can simply refer to Left and Right and not
/// be concerned with the number that is assigned to the value.
/// <see cref="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum"/>
/// </summary>
public enum Paddle { Left, Right };

/// <summary>
/// A separate enum that is used to indicate which direction
/// to rotate or move the "Paddle" on the current axis.
/// Direction is also used in the MoveSphere script.
/// </summary>
public enum Direction { Left, Right, Up, Down, None };

/// <summary>
/// The user can rotate and move the paddle control.
/// This script is used by both the Left and Right Paddle.
/// Each Paddle has their own instance of this class - 
/// they do not share any data.
/// </summary>
public class PaddleControl : MonoBehaviour
{
    // The following two fields need to be 
    // public since we want these values 
    // visible in the Inspector so that 
    // the Game Designer can change the values.

    /// <summary>
    /// The amount to rotate each time the left (left arrow or the a key) 
    /// or to the right ( right arrow or the d key) is
    /// detected as pressed down.
    /// Note that the Left or Right Ctrl key must also be pressed
    /// which indicates which paddle to move.
    /// </summary>
    public float AngleIncrement = 1f;


    /// <summary>
    /// The amount to move the paddle up and down the current axis 
    /// each time the up (up arrow or the w key) 
    /// or to the down ( down arrow or the s key) is
    /// detected as pressed down.
    /// Note that the Left or Right Ctrl key must also be pressed
    /// which indicates which paddle to move.
    /// </summary>
    public float TranslateIncrement = 1f;


    /// <summary>
    /// An enum must be initialized when declared. 
    /// By default the Paddle is set to the Left.
    /// The Game Designer would set each paddle to Left or Right
    /// within the Inspector
    /// </summary>
    public Paddle paddle = Paddle.Left;

    // The following fields are made public
    // simply so that you can observe the changes
    // in the Inspector. Normally these fields
    // would be labelled private as the inspector
    // would need to change these fields.

    /// <summary>
    /// Is the user holding down the key that is
    /// assigned as the Left Modifier?
    /// In the example the Left Ctrl key is the 
    /// Left Modifier.
    /// </summary>
    public bool LeftModifierKeyDown = false;

    /// <summary>
    /// Is the user holding down the key that 
    /// represents the Right Modifier?
    /// In the examples the key used to represent the 
    /// Right Modifier is actually the Left Alt Key as 
    /// it is easier to control Left and Right with the Left hand
    /// and let the right hand to move the arrow keys.
    /// </summary>
    public bool RightModifierKeyDown = false;
  


    /// <summary>
    /// As usual Do NOT change
    /// any code in the Update.
    /// However you should definitely study the Update
    /// and follow the Methods that it calls.
    /// The Update will 
    /// <list>
    /// <item>Determine if the Left Ctrl or Left Alt key is being pressed</item>
    /// <item>Determine if the user wants to turn the paddle (Rotate) </item>
    /// <item>See if the user wants to move (Translate) the paddle</item>
    /// <item>See if the the Left Ctrl or Left Alt key have been released</item>
    /// </list>
    /// </summary>
    void Update()
    {
        // Is the Left and/or Right Modifier key pressed?
        CheckModifierKeyDown();

        // Check to see if the user wants to turn the paddle
        ChoosePaddleForRotation();

        // Check to see if the user wants to move the paddle
        ChoosePaddleForTranslate();

        // Has the Left and/or Right Modifier key been released?
        CheckModifierKeyReleased();
    }

    /// <summary>
    /// In both Choose methods, this one and the 
    /// one following, the method 
    /// <list>
    /// <item>selects a direction based on the key</item>
    /// <item>Checks that the correct modifier key is pressed for each paddle 
    /// (LeftModifierKeyDown for Left Paddle, 
    /// RightModifierKeyDown for Right Paddle)
    /// </item>
    /// <item>Calls the TranslatePaddle method with the chosen Direction</item>
    /// </list>   
    /// </summary>
    private void ChoosePaddleForTranslate()
    {
        Direction d = Direction.None;
        
        // User clicks the Down arrow to move right on the x-axis
        if (Input.GetKey(KeyCode.DownArrow))
            d = Direction.Down;
        // User clicks the Up arrow to move left on the x-axis
        else if (Input.GetKey(KeyCode.UpArrow))
            d = Direction.Up;

        // If the user did not click either Up or Down then 
        // exit this method now
        if (d == Direction.None)
            return;

        // Only call the Translate Paddle method if
        // the current paddle is the Left Paddle and 
        // the LeftModifierKeyDown is pressed down.
        if (paddle == Paddle.Left && LeftModifierKeyDown)
            TranslatePaddle(d);

        // Only call the Translate Paddle method if
        // the current paddle is the Right Paddle and 
        // the RightModifierKeyDown is pressed down.
        else if (paddle == Paddle.Right && RightModifierKeyDown)
            TranslatePaddle(d);

    }


    /// <summary>
    /// Same comments for ChoosePaddleForRotation 
    /// as ChoosePaddleForTranslate.
    /// The only difference is that the RotatePaddle
    /// method is called with the chosen Direction.
    /// </summary>
    private void ChoosePaddleForRotation()
    {
        Direction d = Direction.None;
        if (Input.GetKey(KeyCode.LeftArrow))
            d = Direction.Left;
        else if (Input.GetKey(KeyCode.RightArrow))
            d = Direction.Right;

        if (d == Direction.None)
            return;

        if (paddle == Paddle.Left && LeftModifierKeyDown)
            RotatePaddle(d);
        else if (paddle == Paddle.Right && RightModifierKeyDown)
            RotatePaddle(d);
    }

    /// <summary>
    /// The purpose of this method is to 
    /// identify which paddle to apply the current 
    /// arrow key commands (rotate, translate).
    /// </summary>
    private void CheckModifierKeyDown()
    {
        // If the LeftModifierKeyDown has not already been pressed then 
        // check to see if if is now being pressed.
        // If both of those conditions are true then set the 
        // LeftModifierKeyDown field to true, 
        // and set the RightModifierKeyDown to false
        if (!LeftModifierKeyDown && Input.GetKey(KeyCode.LeftControl))
        {
            LeftModifierKeyDown = true;
            RightModifierKeyDown = false;
        }
        // Same logic as above except for the RightModifierKeyDown
        else
        if (!RightModifierKeyDown && Input.GetKey(KeyCode.LeftAlt))
        {

            RightModifierKeyDown = true;
            LeftModifierKeyDown = false;
        }
    }


    /// <summary>
    /// The purpose of this method is to notify the game that the
    /// user is no longer holding down the modifier key that had 
    /// been held down.
    /// </summary>
    private void CheckModifierKeyReleased()
    {
        if (LeftModifierKeyDown && !Input.GetKey(KeyCode.LeftControl))
            LeftModifierKeyDown = false;
        if (RightModifierKeyDown && !Input.GetKey(KeyCode.LeftAlt))
            RightModifierKeyDown = false;

        // Following statement clears the visible Console Log in the Game View
        Debug.Log(string.Empty);

    }

    /// <summary>
    /// This is one of the two methods that you will complete
    /// for this script.
    /// <list>
    /// <item>You need to move the paddle by the TranslateIncrement field.
    /// (See lines 47-55 in this script)</item>
    /// <item>Do NOT change the TranslateIncrement field.</item>
    /// <item>Move the paddle in a positive direction on the x axis 
    /// if the direction is Down</item>
    /// <item>Move the paddle in a negative direction on the x axis 
    /// if the direction is Up</item>
    /// </list>
    /// </summary>
    /// <param name="d">The Direction value which for this method
    /// will be either Direction.Up or Direction.Down</param>
    public void TranslatePaddle(Direction d)
    {
        /// YOUR WORK GOES HERE
        /// Save the TranslateIncrement to a local variable
        float temp = TranslateIncrement;
        /// Check the direction. If it is Left then negate the value of the local variable from the step above
        if (d == Direction.Down)
        {
            temp = -temp;
        }
        /// Move the Paddle along the X-axis by the local variable 
        /// See Comments below re this action
        transform.Translate(temp, 0, 0);

        /// Comments to help with the last step (Move the Paddle along the X-axis by the local variable )
        /// To move the Paddle you will want to use Translate rather than 
        /// Vector3 math because we need to move in World Coordinates.
        /// See: https://docs.unity3d.com/ScriptReference/Transform.Translate.html
        /// Look at the second description for 
        /// public void Translate(Vector3 translation, Space relativeTo = Space.Self);
        /// This provides an example. It is important to use this version because
        /// as we rotate our paddle around we still want to keep the same orientation
        /// to the x axis. 
    }
    /// <summary>
    /// This is the other method that you will complete.
    /// It is very similar to Translate Paddle. 
    /// The only differences are that 
    /// <list>
    /// <item>the paddle is being rotated</item>
    /// <item>the method determines if the direction is left or right </item>
    /// </list>
    /// 
    /// Rotate the paddle in the specified direction 
    /// by the AngleIncrement. 
    /// I think that this version of Rotate is the easiest to use
    /// public void Rotate(Vector3 axis, float angle, Space relativeTo = Space.Self);
    /// Leave off the third parameter as we want the default value (Space.Self)
    /// Look at the documentation in the link below.
    /// <see cref = "https://docs.unity3d.com/ScriptReference/Transform.Rotate.html" />
    /// </summary>
    /// <param name="d">The direction, left or right</param>
    public void RotatePaddle(Direction d)
    {
        /// Save the AngleIncrement to a local variable
        float temp = AngleIncrement;
        /// Check the direction. If it is to the Right then negate the value of the local variable from the step above
        if (d == Direction.Right)
        {
            temp = -temp;
        }
        /// Rotate the Paddle along the Y-axis by the local variable
        transform.Rotate(0, temp, 0);
    }
}
