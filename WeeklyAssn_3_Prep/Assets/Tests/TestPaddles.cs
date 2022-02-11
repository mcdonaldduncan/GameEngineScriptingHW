using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Tests;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestPaddles
{
    private PaddleControl paddleLeftControl;
    private Rootobject ro;

    /// <summary>
    /// Using the Mathf methods to calculate number of 
    /// degrees in a circle
    /// </summary>
    private readonly int numDegreesCircle = 
        Convert.ToInt32(Mathf.Rad2Deg * Mathf.PI*2);

   

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
            SceneManager.LoadSceneAsync(ro.SceneName, loadSceneParameters);
        }
    }


    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        GameObject temp = GameObject.Find("LeftPaddle");
        if (temp != null)
        {
            paddleLeftControl = temp.GetComponent<PaddleControl>();
        }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.SetActiveScene(arg0);
    }


  
    [UnityTest]
    public IEnumerator TestTranslateUp()
    {
        if (paddleLeftControl != null && ro != null)
        {
            paddleLeftControl.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            Vector3 nextPos = TranslateAction(Direction.Up, out Vector3 expected);
            string errorMessage = "Error in the PaddleControl Script, TranslatePaddle method using Direction.Down: The expected result is:{0}, but the result from using PaddleControl is:{1}";
            Assert.AreEqual(expected, nextPos, errorMessage, expected, nextPos);
        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator TestTranslateDown()
    {
        if (paddleLeftControl != null && ro != null)
        {
            paddleLeftControl.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            Vector3 nextPos = TranslateAction(Direction.Down, out Vector3 expected);
            string errorMessage = "Error in the PaddleControl Script, TranslatePaddle method using Direction.Down: The expected result is:{0}, but the result from using PaddleControl is:{1}";
            Assert.AreEqual(expected, nextPos, errorMessage, expected, nextPos);
        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator TestRotateLeft()
    {
        if (paddleLeftControl != null && ro != null)
        {
            paddleLeftControl.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            RotateOperation(Direction.Left);
        }


        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    [UnityTest]
    public IEnumerator TestRotateRight()
    {
        if (paddleLeftControl != null && ro != null)
        {
            paddleLeftControl.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            RotateOperation(Direction.Right);
        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }



    private void RotateOperation(Direction d)
    {
        float angleIncrement = ro.paddleTester.AngleIncrement;
        paddleLeftControl.AngleIncrement = angleIncrement;
        
        Quaternion temp = paddleLeftControl.transform.localRotation;
        float nextAngleIncrement = temp.y + (d == Direction.Left ? 1 : -1) * angleIncrement;
        if (nextAngleIncrement >= numDegreesCircle)
            nextAngleIncrement -= numDegreesCircle;
        else if (nextAngleIncrement < 0)
            nextAngleIncrement += numDegreesCircle;

        Quaternion expected = Quaternion.AngleAxis(nextAngleIncrement, Vector3.up);


        paddleLeftControl.RotatePaddle(d);
        Quaternion nextRotation = paddleLeftControl.transform.localRotation;
        
        string errorMessage = "Error in the TestRotateLeft Script, RotatePaddle method using Direction.Left: The expected angle is:{0}, but the result from using RotatePaddle is:{1}";
        float expectedMag = Mathf.Round(expected.eulerAngles.magnitude);
        float nextRotMag = Mathf.Round(nextRotation.eulerAngles.magnitude);

        Assert.AreEqual(expectedMag, nextRotMag, errorMessage, expectedMag, nextRotMag);

    }

    

    private Vector3 TranslateAction(Direction d, out Vector3 expected)
    {
        
        float increment = ro.paddleTester.TranslateIncrement;
        Vector3 origin = paddleLeftControl.transform.position;
        paddleLeftControl.TranslateIncrement = increment;
        int mult = d == Direction.Down ? -1 : 1;
        expected = origin + mult * new Vector3(increment, 0, 0);
        paddleLeftControl.TranslatePaddle(d);
        Vector3 nextPos = paddleLeftControl.transform.position;
        return nextPos;
    }


}
