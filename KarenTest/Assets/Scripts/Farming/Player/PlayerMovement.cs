using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    public Vector2 position;
    public int movementIncrement;

    public PlayerMovement(Vector2 startingPos, int movementIncrement)
    {
        this.position = startingPos;
        this.movementIncrement = movementIncrement;
    }

    // Command Pattern
    public void MoveUp(float Input)
    {
        position.y += movementIncrement * Input;
        

    }

    public void MoveDown(float Input)
    {
        position.y += movementIncrement * Input;
    }

    public void MoveLeft(float Input)
    {
        position.x += movementIncrement * Input;
    }

    public void MoveRight(float Input)
    {
        position.x  += movementIncrement * Input;
    }
}
