using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flop : MonoBehaviour
{
    private const int START_AMOUNT_DEALT = 3;
    private const int END_AMOUNT_DEALT = 5;
    public List<Card> flop = new List<Card>(END_AMOUNT_DEALT);

    public void DealInitial()
    {
        for(int i = 0; i < START_AMOUNT_DEALT; ++i)
        {
            flop.Add(Deck.deck[Hand.deal_counter]);
            Hand.deal_counter++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DealInitial();
        PositionCards();
    }

    public void PositionCards()
    {
        int counter = 0;
        foreach(Card card in flop)
        {
            card.card.transform.localPosition = setCardPos(counter);
            counter++;
        }
    }

    public void PositionCardsReset()
    {
        int counter = 0;
        foreach (Card card in flop)
        {
            card.card.transform.localPosition = setCardPos();
            counter++;
        }
    }

    private Vector3 setCardPos(int cardNum)
    {
        return new Vector3(-250.0f + 125.0f * cardNum, 0.0f, 1.0f);
    }

    public Vector3 setCardPos()
    {
        return new Vector3(1000.0f, 0.0f, 1.0f);
    }

    public void DealAnotherCard()
    {
        flop.Add(Deck.deck[Hand.deal_counter]);
        Hand.deal_counter++;
        TurnManagement.card_counter++;
        PositionCards();
        Debug.Log(Hand.deal_counter);
    }
}
