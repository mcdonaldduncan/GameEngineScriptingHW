using System.Collections;
using System.Collections.Generic;
using Assets.Tests;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestFollower
{

    private FollowPlayer followTester;
    private Rootobject ro;


    public static bool PassTests;


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
        GameObject temp = GameObject.Find("Camera");

        if (temp != null)
            followTester = temp.GetComponent<FollowPlayer>();


    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.SetActiveScene(arg0);
        PassTests = true;
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    
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

            if (expected != updatedState)
                PassTests = false;
            Assert.AreEqual(expected, updatedState, errorMessage, expected, updatedState);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    
    public IEnumerator TestUpdateLastSubjectPosition()
    {
        if (followTester != null && ro != null)
        {
            
            float[] lastPosition = ro.followerTester.testLastSubjectPosition;
            Vector3 vecLastPos = new Vector3(lastPosition[0], lastPosition[1], lastPosition[2]);

            
            followTester.UpdateLastSubjectPosition(vecLastPos);
            Vector3 lastSubjectPosition = followTester.lastSubjectPosition;

            string errorMessage = "Error in the Follow Player Script, UpdateLastSubjectPosition method: The expected result is:{0}, but the result from using UpdateLastSubjectPosition is:{1}";

            if (vecLastPos != lastSubjectPosition)
                PassTests = false;

            Assert.AreEqual(vecLastPos, lastSubjectPosition, errorMessage, vecLastPos, lastSubjectPosition);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(.1f);
    }
}
