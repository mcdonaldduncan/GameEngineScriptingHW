using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    /// <summary>
    /// The sprites that represent the cards in a deck
    /// </summary>
    public Sprite[] cardSprites;

    /// <summary>
    /// Sprite that is used to show the card back
    /// </summary>
    public Sprite cardBack;

    /// <summary>
    /// The amount of time it takes for the dealer to 
    /// put a card on the table
    /// </summary>
    public float dealLag = .25f;

    public Card[] deck;
    public Card[] hand;

    private GameObject[] cardGOs;

    public int currentCardPosition { get; set; }

    public List<SpriteRenderer> allSprites =>
        cardGOs.Select(c => 
        c.GetComponent<SpriteRenderer>()).ToList();

    public GameObject GetCardInHandAtPosition(int pos) => cardGOs[pos];
    
    /// <summary>
    /// The CardInformation which references the sprite
    /// to a specialized Card Class
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        InitializeDeck();
        InitializeCardGOsPositions();
        Deal();

    }

    public void Deal()
    {
        CreateHand();
        StartCoroutine(DealHand());
    }

    /// <summary>
    /// Save the child Game Objects, that will
    /// hold the card, to an array of GameObjects
    /// </summary>
    private void InitializeCardGOsPositions()
    {
        // transform.childCount tells us the number of 
        // child objects that belong to the 
        // gameobject that the script is attached to 
        // which in this case is the Table
        int count = this.transform.childCount;

        // Initialize the size of the array so that it 
        // can hold all of the GameObjects
        cardGOs = new GameObject[count];

        // Initialize each element so that each of them 
        // point to one of the child objects
        for (int i = 0; i < count; i++)
        {
            cardGOs[i] = this.transform.GetChild(i).gameObject;
        }
    }


    /// <summary>
    /// Create a hand from the remaining cards in the deck. 
    /// A hand has up to the number of card Game Objects on the Table.
    /// </summary>
    private void CreateHand()
    {
        // Save the size of the hand and the size of the deck
        // to local variables which are used to fill the 
        // positions on the table.
        int maxSizeDeck = this.cardSprites.Length;
        int maxSizeHand = this.cardGOs.Length;

        // Create an array that is the size of the maximum size of the hand
        hand = new Card[maxSizeHand];

        // Keep track of the last i value so that this can be used by the 
        // UpdateCurrentCardPosition method.

        int lastI = -1;
        int lastJ = -1;
        // Fill each element of the hand from the current position in the deck
        // NOTE: i and j are different values.
        // j has to start from zero because that is used to address each element of the hand
        // i starts from wherever the currentCardPosition is
        for (int i = currentCardPosition, j = 0; j < maxSizeHand && i < maxSizeDeck; ++i, ++j)
        {
            hand[j] = deck[i];
            // Update lastJ with the current value of j
            lastI = i;
            lastJ = j;
        }

        UpdateCurrentCardPosition(lastJ + 1);
    }

   
    /// <summary>
    /// Deals the hand after displaying the card backs.
    /// This is a coroutine in order to introduce a delay
    /// so that the user (game player) can see the cards being dealt.
    /// </summary>
    /// <returns>Nothing</returns>
    private IEnumerator DealHand()
    {
        // Turn over the cards and delay before flipping the cards over
        ShowCardBacks();
        yield return new WaitForSeconds(dealLag * hand.Length);

        // Flip the cards over with a delay of dealLag seconds between cards
        WaitForSeconds wait = new WaitForSeconds(dealLag);
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] != null)
            {
                cardGOs[i].GetComponent<SpriteRenderer>().sprite = hand[i].sprite;
                yield return wait;
            }

        }
    }


    /// <summary>
    /// Used to initialize the deck array with 
    /// Card instances to handle all of the cardSprites
    /// </summary>
    private void InitializeDeck()
    {
        deck = new Card[cardSprites.Length];
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = new Card(cardSprites[i]);

        }

    }


    /// <summary>
    /// A new hand is dealt each time the Space Bar is 
    /// pressed.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (IsGameOver())
                Debug.Log("Game is over!");
            else
                Deal();
    }


    /// <summary>
    /// Problem 1: Update the currentCardPosition by adding the parameter advanceBy
    /// to the currentCardPosition.
    /// currentCardPosition cannot be larger then the size of the deck.
    /// </summary>
    /// <param name="advanceBy">Advance the current card position by this value</param>
    public void UpdateCurrentCardPosition(int advanceBy)
    {
        // Advance the currentCardPosition by advanceBy
        

        // Debug statement provided so that you can see the currentCardPosition change
        Debug.LogFormat("currentCurePosition = {0}", currentCardPosition);
    }

    /// <summary>
    /// Problem 2: Return true if the last card has been dealt, false if there are 
    /// still more cards to deal.
    /// IE The last card equals the count of cards in the deck.
    /// HINT: Do NOT assume that the deck has 52 cards. Always use the 
    /// Length property of the deck array.
    /// </summary>
    /// <returns>True if the last card has been dealt</returns>
    public bool IsGameOver()
    {
        // Return true if the currentCardPosition is the same length 
        // as the deck, don't assume that it is 52!
        // The following line will be true or false depending upon 
        // the question above.
        return false;
    }


    /// <summary>
    /// Problem 3:
    /// Use a For loop to set the sprite of each cardGOs
    /// to the CardBack.
    /// NOTE: CardBack is a class variable.
    /// Look at the Deal Hand method for help in solving this problem
    /// </summary>
    public void ShowCardBacks()
    {
        // Set the sprite to the class variable CardBack.
        // Do not assume the size of the array. Do not assume 
        // that there are only four card positions.
    }

    /// <summary>
    /// Problem 4:
    /// Sum the value of all of the cards and return the sum.
    /// </summary>
    /// <returns>The sum of all cards</returns>
    public int sumOfHand()
    {
        int sum = 0;
        // Loop through the array to get the sum of all of the values 
        // of the cards.
        return sum;
    }

    /// <summary>
    /// Problem 5: 
    /// Optional/Advanced/Double Points.
    /// Find the position of the currentCard in the cardGos array.
    /// Get the card instance for the same position in the hands array.
    /// Display the card information using Debug.Log by calling the ToString
    /// method on Card and return the card instance.
    /// </summary>
    /// <param name="currentCard">Return the card instance</param>
    public Card DisplayCardInfo(GameObject currentCard)
    {
        int index = -1;
        for (int i = 0; i < cardGOs.Length; i++)
        {
            if (currentCard == cardGOs[i])
            {
                index = i;
            }
        }
        Card c = hand[index];
        
        // Find the Card instance that corresponds with the 
        // GameObject 

        Debug.LogFormat($"Card is: {c.ToString()}");
        return c;
    }
}
