using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static List<Card> deck = new List<Card>();

    // Generates a Deck
    // test
    public static void GenerateDeck(List<Card> deck)
    {
        foreach(Card.SUIT suit in Enum.GetValues(typeof(Card.SUIT)))
        {
            foreach (Card.CARD_VALUE card_value in Enum.GetValues(typeof(Card.CARD_VALUE)))
            {
                deck.Add(new Card(suit, card_value, GameObject.Find(card_value.ToString() + "_" + suit.ToString())));
            }
        }
    }

    public static List<Card> ShuffleDeck(int shuffleLoopLength)
    {
        for (int i = 0; i < shuffleLoopLength; ++i)
        {
            int rand1 = UnityEngine.Random.Range(0, deck.Count - 1);
            int rand2 = UnityEngine.Random.Range(0, deck.Count - 1);
            while(rand1 == rand2)
            {
                rand2 = UnityEngine.Random.Range(0, deck.Count - 1);
            }
            Card hold = deck[rand1];
            deck[rand1] = deck[rand2];
            deck[rand2] = hold;
        }
        return deck;
    }

    private void Awake()
    {
        GenerateDeck(deck);
        Deck.ShuffleDeck(10000);
    }
}
