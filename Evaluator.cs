using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Evaluator : MonoBehaviour
{
    private const int HAND_LIMIT = 5;
    private const int HAND_SIZE = 7;
    private const int ITERATION_COUNT = 3;
   public bool isFlush(List<Card> hand)
    {
        int counter = 0;
        for(int i = 0; i < hand.Count - 1; ++i)
        {
            if(hand[i].suit == hand[i + 1].suit)
            {
                counter++;
                continue;
            }
            if(counter == HAND_LIMIT)
            {
                break;
            }
            counter = 0;
        }
        return counter == HAND_LIMIT;
    }

    public static List<Card> sortHandByValue(List<Card> hand)
    {
        return hand.OrderByDescending(c => (int)(c.card_value)).ToList();
    }

    public int getCardVal(Card card)
    {
        return (int)card.card_value;
    }

    public bool isStraight(List<Card> hand)
    {
        sortHandByValue(hand);

        for(int i = 0; i < ITERATION_COUNT; ++i)
        {
            if(getCardVal(hand[0 + i]) == getCardVal(hand[1 + i]) - 1 && getCardVal(hand[1 + i]) == getCardVal(hand[2 + i]) - 1 && getCardVal(hand[2 + i]) == getCardVal(hand[3 + i]) - 1 && getCardVal(hand[3 + i]) == getCardVal(hand[4 + i]) - 1)
            {
                return true;
            }
        }
        return false;
    }
}
