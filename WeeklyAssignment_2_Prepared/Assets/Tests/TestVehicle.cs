using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Tests;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestVehicle
{
    private DriveVehicle vehicleTester;
    private FollowPlayer followTester;
    private Rootobject ro;
    
    [SetUp]
    public void Setup()
    {
        ro = Utility.InitializeConfigValues();
        if (ro == null)
            Assert.Fail("Startup error: cannot load Configuration File");

        Scene curScene = SceneManager.GetActiveScene();
        if (!(curScene != null && curScene.name == ro.SceneName))
        {
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Additive);
            SceneManager.LoadScene(ro.SceneName, loadSceneParameters);
        }
    }


    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        GameObject temp = GameObject.Find("Veh_Car_Blue_Z");

        if (temp != null)
            vehicleTester = temp.GetComponent<DriveVehicle>();

        temp = GameObject.Find("Camera");

        if (temp != null)
            followTester = temp.GetComponent<FollowPlayer>();

    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.SetActiveScene(arg0);
    }



    

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestMoveVehicle()
    {

        if (vehicleTester != null && ro != null)
        {
            float increment = ro.vehicleTester.MoveVehicle;
            
            Vector3 origin = vehicleTester.transform.position;
            Vector3 expected = origin + Vector3.back * increment * vehicleTester.Speed;
            Vector3 nextPos  = vehicleTester.CalculateNextPosition(origin,increment);
            string errorMessage = "Error in the DriveVehicle Script, MoveVehicle method: The expected result is:{0}, but the result from using CalculateNextPosition is:{1}";
            Assert.AreEqual(expected, nextPos, errorMessage, expected, nextPos);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator  TestUpdateSpeed_WithIncrease()
    {

        if (vehicleTester != null && ro != null)
        {
            float increment = ro.vehicleTester.Up;

           
            float expected = vehicleTester.Speed + increment;
            float updatedSpeed = DriveVehicle.UpdateSpeed(vehicleTester.Speed, increment);
            string errorMessage = "Error in the DriveVehicle Script, UpdateSpeed method: The expected result is:{0}, but the result from using UpdateSpeed is:{1}";
            Assert.AreEqual(expected, updatedSpeed, errorMessage, expected, updatedSpeed);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestUpdateSpeed_WithLargeNegative()
    {

        if (vehicleTester != null && ro != null)
        {
            float increment = ro.vehicleTester.Down;


            float expected = 0f;
            float updatedSpeed = DriveVehicle.UpdateSpeed(vehicleTester.Speed, increment);
            string errorMessage = "Error in the DriveVehicle Script, UpdateSpeed method: The expected result is:{0}, but the result from using UpdateSpeed is:{1}";
            Assert.AreEqual(expected, updatedSpeed, errorMessage, expected, updatedSpeed);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestSetPauseState()
    {

        if (vehicleTester != null && ro != null)
        {
            bool IsPaused = ro.vehicleTester.IsPaused;


            bool expected = !IsPaused;
            bool updatedState = DriveVehicle.SetPauseState(IsPaused);
            string errorMessage = "Error in the DriveVehicle Script, SetPauseState method: The expected result is:{0}, but the result from using SetPauseState is:{1}";
            Assert.AreEqual(expected, updatedState, errorMessage, expected, updatedState);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestCheckForStop()
    {

        if (vehicleTester != null && ro != null)
        {

            bool expected = ro.vehicleTester.IsPastStop;
            Vector3 currentPosition = Vector3.zero;
            Vector3 stoppingPosition = Vector3.forward;

            bool updatedState = DriveVehicle.CheckForStop(currentPosition, stoppingPosition);
            string errorMessage = "Error in the DriveVehicle Script, CheckForStop method: The expected result is:{0}, but the result from using CheckForStop is:{1}";
            Assert.AreEqual(expected, updatedState, errorMessage, expected, updatedState);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestIsSubjectPaused()
    {
        if (followTester != null && ro != null)
        {
            float[] current = ro.followerTester.currentPosition;
            float[] lastPosition = ro.followerTester.lastPosition;

            Vector3 vecCurrent = new Vector3(current[0], current[1], current[2]);
            Vector3 vecLastPos = new Vector3(lastPosition[0], lastPosition[1], lastPosition[2]);

            bool expected = true;
            bool updatedState = FollowPlayer.IsSubjectPaused(vecCurrent, vecLastPos);
            string errorMessage = "Error in the Follow Player Script, IsSubjectPaused method: The expected result is:{0}, but the result from using IsSubjectPaused is:{1}";

           
            Assert.AreEqual(expected, updatedState, errorMessage, expected, updatedState);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestUpdateLastSubjectPosition()
    {
        if (followTester != null && ro != null)
        {

            float[] lastPosition = ro.followerTester.testLastSubjectPosition;
            Vector3 vecLastPos = new Vector3(lastPosition[0], lastPosition[1], lastPosition[2]);


            followTester.UpdateLastSubjectPosition(vecLastPos);
            Vector3 lastSubjectPosition = followTester.lastSubjectPosition;

            string errorMessage = "Error in the Follow Player Script, UpdateLastSubjectPosition method: The expected result is:{0}, but the result from using UpdateLastSubjectPosition is:{1}";

           

            Assert.AreEqual(vecLastPos, lastSubjectPosition, errorMessage, vecLastPos, lastSubjectPosition);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

}

 