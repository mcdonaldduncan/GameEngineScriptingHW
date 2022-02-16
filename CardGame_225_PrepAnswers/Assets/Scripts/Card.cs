using UnityEngine;
using System;

public enum Suit { CLUB, DIAMOND, HEART, SPADE}
public class Card
{
    /// <summary>
    /// A reference to the sprite used 
    /// for this card.
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// The suit for the card
    /// </summary>
    public Suit suit;

    /// <summary>
    /// The numeric value of the card
    /// </summary>
    public int value;

    /// <summary>
    /// True if the card is Jack, King, Queen
    /// </summary>
    public bool isFaceCard;

    public string faceCardName;

    /// <summary>
    /// True if the card has a value of 1 which is what is 
    /// used in the original sprite.
    /// </summary>
    public bool isAce;

    public Card(Sprite spriteParam)
    {
        this.sprite = spriteParam;
        ParseCardName(this.sprite.name);
        
    }

    /// <summary>
    /// Extract the suit, value and potential face card name
    /// from the name property of the sprite.
    /// </summary>
    /// <param name="name">The name of the sprite</param>
    private void ParseCardName(string name)
    {
        string[] tokens = name.Split('-');
        if (tokens.Length < 2)
            throw new Exception("The sprite name must have at least two parts!");

        if (tokens.Length == 2)
            Debug.LogFormat("Standard Card: {0}", name);
        else
            Debug.LogFormat("Royal Card: {0}", name);


        suit = (Suit)Enum.Parse(typeof(Suit), tokens[0]);
        if (Int32.TryParse(tokens[1], out int temp))
            value = temp;

        if (value == 1)
            isAce = true;

        if (tokens.Length == 3)
        {
            faceCardName = tokens[2];
            isFaceCard = true;
        }
    }

    public override string ToString()
    {
        if (isFaceCard)
            return $"{faceCardName} of {suit}";
        else if (isAce)
            return $"Ace of {suit}";
        else
            return $"{value} of {suit}";
    }
}