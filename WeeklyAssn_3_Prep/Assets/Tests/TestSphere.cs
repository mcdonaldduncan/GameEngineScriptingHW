using System.Collections;
using System.Collections.Generic;
using Assets.Tests;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestSphere
{
    private MoveSphere moveSphereControl;

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
            SceneManager.LoadSceneAsync(ro.SceneName, loadSceneParameters);
        }
    }


    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        GameObject temp = GameObject.Find("Sphere");
        if (temp != null)
            moveSphereControl = temp.GetComponent<MoveSphere>();
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.SetActiveScene(arg0);
    }

    [UnityTest]
    public IEnumerator TestChangeDirectionUpWithAdjustment()
    {
        if (moveSphereControl != null && ro != null)
        {
            moveSphereControl.speed = 50;
            
            float increment = ro.sphereTester.increment;
            moveSphereControl.increment = increment;
            float expected = -(moveSphereControl.speed + increment);
            moveSphereControl.AdjustSpeed(Direction.Up, moveSphereControl.speed);
            moveSphereControl.ChangeDirectionBasedOnSpeed();
            float nextSpeed = moveSphereControl.speed;

            string errorMessage = "Error in the MoveSphere Script, " + 
                "ChangeDirectionBasedOnSpeed method using Direction.Right and there was a speed adjustment: The expected result is:{0}, but the result from using ChangeDirectionBasedOnSpeed is:{1}";
            Assert.AreEqual(expected, nextSpeed, errorMessage, expected, nextSpeed);
        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    //[UnityTest]
    [UnityTest]
    public IEnumerator TestChangeDirectionUp_WithOutAdjustment()
    {
        if (moveSphereControl != null && ro != null)
        {
            moveSphereControl.speed = 50;

            
            float expected = -moveSphereControl.speed;
            moveSphereControl.ChangeDirectionBasedOnSpeed();
            float nextSpeed = moveSphereControl.speed;

            string errorMessage = "Error in the MoveSphere Script, " +
                "ChangeDirectionBasedOnSpeed method using Direction.Right no speed adjustment: The expected result is:{0}, but the result from using ChangeDirectionBasedOnSpeed is:{1}";
            Assert.AreEqual(expected, nextSpeed, errorMessage, expected, nextSpeed);
        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }


    [UnityTest]
    public IEnumerator TestChangeDirectionDown_WithAdjustment()
    {
        if (moveSphereControl != null && ro != null)
        {
            moveSphereControl.speed = -50;

            float increment = ro.sphereTester.increment;
            moveSphereControl.increment = increment;
            float expected = -(moveSphereControl.speed + increment);
            moveSphereControl.AdjustSpeed(Direction.Down, moveSphereControl.speed);
            moveSphereControl.ChangeDirectionBasedOnSpeed();
            float nextSpeed = moveSphereControl.speed;

            string errorMessage = "Error in the MoveSphere Script, " +
                "ChangeDirectionBasedOnSpeed method using Direction.Left and there was a speed adjustment: The expected result is:{0}, but the result from using ChangeDirectionBasedOnSpeed is:{1}";
            Assert.AreEqual(expected, nextSpeed, errorMessage, expected, nextSpeed);
        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    //[UnityTest]
    [UnityTest]
    public IEnumerator TestChangeDirectionDown_WithOutAdjustment()
    {
        if (moveSphereControl != null && ro != null)
        {
            moveSphereControl.speed = -50;
            float expected = -moveSphereControl.speed;
            moveSphereControl.ChangeDirectionBasedOnSpeed();
            float nextSpeed = moveSphereControl.speed;

            string errorMessage = "Error in the MoveSphere Script, " +
                "ChangeDirectionBasedOnSpeed method using Direction.Left no speed adjustment: The expected result is:{0}, but the result from using ChangeDirectionBasedOnSpeed is:{1}";
            Assert.AreEqual(expected, nextSpeed, errorMessage, expected, nextSpeed);
        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }
}
