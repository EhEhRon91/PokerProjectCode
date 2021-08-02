using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public SUIT suit;
    public CARD_VALUE card_value;
    public string card_name;
    public GameObject card;

    public Card(SUIT suit, CARD_VALUE card_value, GameObject card)
    {
        this.suit = suit;
        this.card_value = card_value;
        card_name = card_value.ToString() + " of " + suit.ToString();
        this.card = card;
    }

    public enum SUIT
    {
        HEARTS,
        DIAMONDS,
        SPADES,
        CLUBS
    }

    public enum CARD_VALUE
    {
        TWO = 0,
        THREE = 1,
        FOUR = 2,
        FIVE = 3,
        SIX = 4,
        SEVEN = 5,
        EIGHT = 6,
        NINE = 7,
        TEN = 8,
        JACK = 9,
        QUEEN = 10,
        KING = 11,
        ACE = 12
    }
}
