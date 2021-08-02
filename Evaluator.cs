using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Evaluator : MonoBehaviour
{
    private const int HAND_LIMIT = 5;
    private const int HAND_SIZE = 7;
    public string hand_result = "";

    public static List<Card> sortHandByValue(List<Card> hand)
    {
        return hand.OrderByDescending(c => (int)(c.card_value)).ToList();
    }

    public int getCardVal(Card card)
    {
        return (int)card.card_value;
    }

    public bool isRoyalFlush(Hand hand)
    {
        sortHandByValue(hand.evaluatedHand);
        hand_result = "ROYAL FLUSH OF " + hand.evaluatedHand[3].suit.ToString() + "'s";
        hand.hand_evaluated = Hand.HAND_EVALUATED.ROYAL_FLUSH;
        return isFlush(hand) && isStraight(hand) && (getCardVal(hand.evaluatedHand[4]) == 12 || getCardVal(hand.evaluatedHand[5]) == 12 || getCardVal(hand.evaluatedHand[6]) == 12); 
    }

    public bool isStraightFlush(Hand hand)
    {
        sortHandByValue(hand.evaluatedHand);
        hand_result = "STRAIGHT FLUSH";
        hand.hand_evaluated = Hand.HAND_EVALUATED.STRAIGHT_FLUSH;
        return isFlush(hand) && isStraight(hand);
    }

    public bool isFlush(Hand hand)
    {
        int counter = 0;
        for(int i = 0; i < hand.evaluatedHand.Count - 1; ++i)
        {
            if(hand.evaluatedHand[i].suit == hand.evaluatedHand[i + 1].suit)
            {
                counter++;
                continue;
            }
            if(counter == HAND_LIMIT)
            {
                hand_result = "FLUSH OF " + hand.evaluatedHand[i].suit.ToString() + "'s";
                break;
            }
            counter = 0;
        }
        hand.hand_evaluated = Hand.HAND_EVALUATED.FLUSH;
        return counter == HAND_LIMIT;
    }

    public bool isStraight(Hand hand)
    {
        sortHandByValue(hand.evaluatedHand);
        hand_result = "STRAIGHT";
        hand.hand_evaluated = Hand.HAND_EVALUATED.STRAIGHT;
        return false;
    }

    public bool isWheelStraight(Hand hand)
    {
        sortHandByValue(hand.evaluatedHand);
        hand_result = "STRAIGHT";
        hand.hand_evaluated = Hand.HAND_EVALUATED.STRAIGHT;
        return false;
    }

    public bool isFourOfAKind(Hand hand)
    {
        sortHandByValue(hand.evaluatedHand);

        for (int i = 0; i < 4; ++i)
        {
            if (getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[1 + i]) && getCardVal(hand.evaluatedHand[1 + i]) == getCardVal(hand.evaluatedHand[2 + i]) && getCardVal(hand.evaluatedHand[2 + i]) == getCardVal(hand.evaluatedHand[3 + i]))
            {
                hand_result = "FOUR OF A KIND WITH " + hand.evaluatedHand[0 + i].card_value.ToString() + "'s";
                hand.hand_evaluated = Hand.HAND_EVALUATED.QUADS;
                return true;
            }
        }
        return false;
    }

    public bool isFullHouse(Hand hand)
    {
        sortHandByValue(hand.evaluatedHand);

        for(int i = 0; i < 3; ++i)
        {
            if(getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[i + 1]) && getCardVal(hand.evaluatedHand[i + 2]) == getCardVal(hand.evaluatedHand[i + 3]) && getCardVal(hand.evaluatedHand[i + 3]) == getCardVal(hand.evaluatedHand[i + 4]))
            {
                hand_result = "FULL HOUSE, " + hand.evaluatedHand[i + 2].card_name.ToString() + " FULL OF " + hand.evaluatedHand[i + 0].ToString() + "'s";
                hand.hand_evaluated = Hand.HAND_EVALUATED.FULL_HOUSE;
                return true;
            }
            if (getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[i + 1]) && getCardVal(hand.evaluatedHand[i + 0]) == getCardVal(hand.evaluatedHand[i + 2]) && getCardVal(hand.evaluatedHand[i + 3]) == getCardVal(hand.evaluatedHand[i + 4]))
            {
                hand_result = "FULL HOUSE, " + hand.evaluatedHand[i + 0].card_name.ToString() + " FULL OF " + hand.evaluatedHand[i + 3].ToString() + "'s";
                hand.hand_evaluated = Hand.HAND_EVALUATED.FULL_HOUSE;
                return true;
            }
        }
        return false;
    }

    public bool isThreeOfAKind(Hand hand)
    {
        sortHandByValue(hand.evaluatedHand);

        for (int i = 0; i < 5; ++i)
        {
            if (getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[1 + i]) && getCardVal(hand.evaluatedHand[1 + i]) == getCardVal(hand.evaluatedHand[2 + i]))
            {
                hand_result = "THREE OF A KIND WITH " + hand.evaluatedHand[0 + i].card_value.ToString() + "'s";
                hand.hand_evaluated = Hand.HAND_EVALUATED.TRIPS;
                return true;
            }
        }
        return false;
    }

    public bool isTwoPair(Hand hand)
    {
        sortHandByValue(hand.evaluatedHand);

        for(int i = 0; i < 4; ++i)
        {
            if(getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[1 + i]) && getCardVal(hand.evaluatedHand[2 + i]) == getCardVal(hand.evaluatedHand[3 + i]))
            {
                hand_result = "TWO PAIR WITH " + hand.evaluatedHand[0 + i].card_value + "'s and " + hand.evaluatedHand[2 + i].card_value + "'s";
                hand.hand_evaluated = Hand.HAND_EVALUATED.TWO_PAIR;
                return true;
            }
        }
        return false;
    }

    public bool isPair(Hand hand)
    {
        sortHandByValue(hand.evaluatedHand);

        for(int i = 0; i < 6; ++i)
        {
            if(getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[1 + i]))
            {
                hand_result = "PAIR WITH " + hand.evaluatedHand[0 + i].card_value + "'s";
                hand.hand_evaluated = Hand.HAND_EVALUATED.PAIR;
                return true;
            }
        }
        return false;
    }

    public int compareTwoHands(Hand hand1, Hand hand2)
    {
        return 0;   
    }
}
