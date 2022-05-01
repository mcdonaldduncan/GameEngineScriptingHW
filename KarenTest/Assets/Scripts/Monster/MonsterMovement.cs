using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement
{
    public MonsterMoveState movestate;

    public float Speed;
    float currentSpeed;
    float currentSpeedY;
    float idleSpeed;
    float offset;
    public int max;

    public Vector2 position;
    public Vector2 wanderpoint;

    public MonsterMovement()
    {
        this.movestate = MonsterMoveState.Finding;
        idleSpeed = 0;
        currentSpeed = Speed;
        //currentSpeedY = Speed;
        offset = 2;
    }

    public void Wander()
    {
        bool isRight = DetermineDirection(position.x, wanderpoint.x);
        //bool isUp = DetermineDirection(position.y, wanderpoint.y);
        bool isStopped = PointReached(position.x, wanderpoint.x);

        if (!isRight)
        {
            isStopped = PointReachedLeft(position.x, wanderpoint.x);
            //if (!isUp)
            //{
            //    //isStopped = PointReachedLeft(position.x, wanderpoint.x) && PointReached(position.y, wanderpoint.y);
            //}
        }

        if (!isStopped)
        {
            position.x += currentSpeed;
            //position.y += currentSpeedY;
        }
        else
        {
            this.movestate = MonsterMoveState.Idle;
        }
    }

    public void DetermineState()
    {
        switch (movestate)
        {
            case (MonsterMoveState.Idle):
                Idle();
                break;
            case (MonsterMoveState.Wandering):
                Wander();
                break;
            case (MonsterMoveState.Occupied):
                Idle();
                break;
            case (MonsterMoveState.Finding):
                ChangeWanderPoint();
                break;
            case (MonsterMoveState.Waiting):
                break;
        }
    }

    void ChangeWanderPoint()
    {
        currentSpeed = Speed;
        float destinationX = GenWanderPoint();
        wanderpoint.x = destinationX;
        float destinationY = GenWanderPoint();
        wanderpoint.y = destinationY;

        bool isRight = DetermineDirection(position.x, wanderpoint.x);
        bool isUp = DetermineDirection(position.y, wanderpoint.y);

        if (!isRight)
        {
            currentSpeed = -currentSpeed;
        }

        if (!isUp)
        {
            //currentSpeedY = -currentSpeedY;
        }

        this.movestate = MonsterMoveState.Wandering;
    }

    float GenWanderPoint()
    {
        float initial = Random.Range(-max, max);

        while (position.x - offset < initial && initial < position.x + offset)
        {
            initial = Random.Range(-max, max);
        }

        return initial;
    }

    bool DetermineDirection(float initial, float target)
    {
        // Means target is to the right
        return (target > initial);
    }

    bool PointReachedLeft(float initial, float target)
    {
        return (target == initial || initial <= target + -currentSpeed * offset);
    }

    bool PointReached(float initial, float target)
    {
        return (target == initial || initial >= target - currentSpeed * offset);
    }

    void Idle()
    {
        this.currentSpeed = idleSpeed;
        this.currentSpeedY = idleSpeed;

        // Will randomly start moving again
        bool ismoving = Randomize();

        if (ismoving)
        {
            this.movestate = MonsterMoveState.Finding;
        }
    }

    bool Randomize()
    {
        int number = Random.Range(0, 1000);
        return number == 1;
    }
}

public enum MonsterMoveState
{
    Wandering,
    Idle,
    Occupied,
    Waiting,
    Finding
}
