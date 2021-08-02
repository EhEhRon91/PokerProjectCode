using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private const int HAND_SIZE = 2;
    public List<Card> hand = new List<Card>(HAND_SIZE);
    public List<Card> evaluatedHand = new List<Card>(7);
    public static int deal_counter = 0;
    private Flop flop;
    private Evaluator evaluator;
    public HAND_EVALUATED hand_evaluated;
    private void DealHand()
    {
        for(int i = 0; i < HAND_SIZE; ++i)
        {
            hand.Add(Deck.deck[deal_counter]);
            deal_counter++;
        }
    }

    public enum HAND_EVALUATED
    {
        ROYAL_FLUSH,
        STRAIGHT_FLUSH,
        QUADS,
        FULL_HOUSE,
        FLUSH,
        STRAIGHT,
        TRIPS,
        TWO_PAIR,
        PAIR,
        HIGH_CARD
    }

    private void PositionHand()
    {
        int counter = 0;
        foreach(Card card in hand)
        {
            card.card.transform.localPosition = setCardPos(gameObject.name, counter);
            counter++;
        }
    }

    private Vector3 setCardPos(string gameObjectName, int cardNum)
    {
        if(gameObjectName == "PlayerHand")
        {
            return new Vector3(-50.0f + 100.0f * cardNum, -190.0f, 1.0f);
        }
        else if(gameObjectName == "Enemy1Hand")
        {
            return new Vector3(-650.0f, -75.0f + 150.0f * cardNum, 1.0f);
        }
        else if(gameObjectName == "Enemy2Hand")
        {
            return new Vector3(-50.0f  + 100.0f * cardNum, 500.0f, 1.0f);
        }
        else if(gameObjectName == "Enemy3Hand")
        {
            return new Vector3(650.0f, -75.0f + 150.0f * cardNum, 1.0f);
        }
        return new Vector3(1000.0f, 0.0f, 1.0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        DealHand();
        PositionHand();
    }

    public void EvaluateHandToString()
    {
        evaluatedHand = hand;
        foreach(Card card in flop.flop)
        {
            evaluatedHand.Add(card);
        }
    }
}
