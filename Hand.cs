using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private const int HAND_SIZE = 2;
    public const int MAX_HAND_SIZE = 7;
    public List<Card> hand = new List<Card>(HAND_SIZE);
    public List<Card> evaluatedHand = new List<Card>(MAX_HAND_SIZE);
    public Card card_to_compare;
    public string hand_result;
    public string hand_result2;
    public HAND_EVALUATED hand_evaluated;

    // May me needed when comparing Full Houses and Two Pair
    public Card card_to_compare_two;

    public static int deal_counter = 0;
    public Flop flop;
    private Evaluator evaluator;
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
        HIGH_CARD,
        PAIR,
        TWO_PAIR,
        THREE_OF_A_KIND,
        FULL_HOUSE,
        WHEEL_STRAIGHT,
        STRAIGHT,
        FLUSH,
        FOUR_OF_A_KIND,
        STRAIGHT_FLUSH,
        ROYAL_FLUSH
    }

    //Helper methods to test
    public void PrintHand()
    {
        Debug.Log("/////" + gameObject.name + "///////");
        foreach(Card card in evaluatedHand)
        {
            Debug.Log(card.card_name);
        }
        Debug.Log("///////////////");
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

    public void GetEvaluatedHand()
    {
        foreach(Card card in hand)
        {
            evaluatedHand.Add(card);
        }
        foreach(Card card in flop.flop)
        {
            evaluatedHand.Add(card);
        }
    }
}
