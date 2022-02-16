using System.Collections;
using System.Collections.Generic;
using Assets.Tests;
using NUnit.Framework;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class CardsTests
{
    private Dealer dealer;
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
        GameObject temp = GameObject.Find("Table");
        if (temp != null)
        {
            dealer = temp.GetComponent<Dealer>();
        }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.SetActiveScene(arg0);
    }

    [UnityTest]
    public IEnumerator TestUpdateCurrentCardPosition()
    {

        if (dealer != null && ro != null)
        {
            int currentPos = dealer.currentCardPosition;
            dealer.UpdateCurrentCardPosition(ro.dealerTester.advanceBy);
            int nextPos = dealer.currentCardPosition;
            string errorMessage = "Error in the Dealer Script, UpdateCurrentCardPosition method: The expected result is:{0}, but the result from using UpdateCurrentCardPosition is:{1}";
            int expected = currentPos + ro.dealerTester.advanceBy;

            Assert.AreEqual(expected, nextPos, errorMessage, expected, nextPos);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(2f);
    }

    [UnityTest]
    public IEnumerator TestIsGameOver()
    {

        if (dealer != null && ro != null)
        {
            int savePos = dealer.currentCardPosition;
            var eDtPos = ro.dealerTester.endDeckTest;
            dealer.currentCardPosition = eDtPos;
            var saveDeck = dealer.deck;
            Card[] newDeck = new Card[eDtPos];
            dealer.deck = newDeck;

            bool atEnd = dealer.IsGameOver();
            int deckLength = dealer.deck.Length;
            bool expected = (deckLength ^ dealer.currentCardPosition) == 0;
            string errorMessage = "Error in the Dealer Script, IsGameOver method: The expected result is:{0}, but the result from using IsGameOver is:{1}";

            dealer.currentCardPosition = savePos;
            dealer.deck = saveDeck;
            Assert.AreEqual(expected, atEnd, errorMessage, expected, atEnd);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(2f);
    }

    [UnityTest]

    public IEnumerator TestDoCardsShowBacks()
    {
        if (dealer != null && ro != null)
        {
            TestUtil.ResetCards(dealer);
            dealer.ShowCardBacks();
            bool result = dealer.allSprites.All(sp => sp.sprite == dealer.cardBack);
            bool expected = true;
            string errorMessage = "Error in the Dealer Script, ShowCardBacks did not turn over all of the cards";

            Assert.AreEqual(expected, result, errorMessage);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(2f);
    }

    [UnityTest]

    public IEnumerator Test_sumOfHand()
    {
        if (dealer != null && ro != null)
        {
            dealer.currentCardPosition = ro.dealerTester.initialCardForSum;
            dealer.Deal();
            int expected = dealer.hand.Select(h => h.value).Sum();
            int result = dealer.sumOfHand();
            
            string errorMessage = "Error in the Dealer Script, sumOfHand returned {0} but {1} was expected";

            Assert.AreEqual(expected, result, errorMessage, result, expected);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(2f);
    }

    
    [UnityTest]

    public IEnumerator Test_DisplayCardInfo()
    {
        if (dealer != null && ro != null)
        {
            int cardGOPosition = ro.dealerTester.displayCardRelPos;
            dealer.currentCardPosition = ro.dealerTester.advanceBy;
            dealer.Deal();
            
            GameObject go = dealer.GetCardInHandAtPosition(cardGOPosition);
            Card result = dealer.DisplayCardInfo(go);
            Card expected = dealer.hand[cardGOPosition];

            string errorMessage = "Error in the Dealer Script, DisplayCardInfo returned {0} but {1} was expected";

            Assert.AreEqual(expected, result, errorMessage, result, expected);

        }

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(2f);
    }

}
