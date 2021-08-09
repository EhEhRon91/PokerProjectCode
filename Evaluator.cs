using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Evaluator : MonoBehaviour
{
    private const int HAND_LIMIT = 5;
    private const int HAND_SIZE = 7;

    public Hand playerHand;
    public Hand enemy1Hand;
    public Hand enemy2Hand;
    public Hand enemy3Hand;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        playerHand.GetEvaluatedHand();
        enemy1Hand.GetEvaluatedHand();
        enemy2Hand.GetEvaluatedHand();
        enemy3Hand.GetEvaluatedHand();
    }

    public static List<Card> sortHandByValue(List<Card> hand)
    {
        return hand.OrderBy(c => (int)(c.card_value)).ToList();
    }

    public static List<Card> sortHandBySuit(List<Card> hand)
    {
        return hand.OrderBy(c => (int)(c.suit)).ToList();
    }

    public int getCardVal(Card card)
    {
        return (int)card.card_value;
    }

    public string HandString(Hand hand)
    {
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.ROYAL_FLUSH)
        {
            return "ROYAL FLUSH OF " + hand.evaluatedHand[3].suit.ToString() + "'s";
        }
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.STRAIGHT_FLUSH)
        {
            return "STRAIGHT FLUSH OF " + hand.evaluatedHand[4].suit.ToString() + "'s WITH A HIGH CARD OF " + hand.card_to_compare.card_value.ToString();
        }
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.FOUR_OF_A_KIND)
        {
            return "FOUR OF A KIND WITH " + hand.card_to_compare.card_value.ToString() + "'s";
        }
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.FLUSH)
        {
            return "FLUSH OF " + hand.card_to_compare.suit.ToString() + "'s WITH HIGH CARD OF " + hand.card_to_compare.card_value.ToString();
        }
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.STRAIGHT)
        {
            return "STRAIGHT WITH A HIGH CARD OF " + hand.card_to_compare.card_value.ToString();
        }
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.WHEEL_STRAIGHT)
        {
            return "STRAIGHT WITH A HIGH OF FIVE";
        }
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.FULL_HOUSE)
        {
            return "FULL HOUSE, " + hand.card_to_compare.card_value.ToString() + "'s FULL OF " + hand.card_to_compare_two.card_value.ToString() + "'s";
        }
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.THREE_OF_A_KIND)
        {
            return "THREE OF A KIND WITH " + hand.card_to_compare.card_value.ToString() + "'s";
        }
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.TWO_PAIR)
        {
            return "TWO PAIR WITH " + hand.card_to_compare.card_value.ToString() + "'s and " + hand.card_to_compare_two.card_value.ToString() + "'s";
        }
        if(hand.hand_evaluated == Hand.HAND_EVALUATED.PAIR)
        {
            return "PAIR WITH " + hand.card_to_compare.card_value.ToString() + "'s";
        }

        hand.evaluatedHand = sortHandByValue(hand.evaluatedHand);
        return "HIGH CARD OF " + hand.evaluatedHand[hand.evaluatedHand.Count - 1].card_value.ToString();
    }

    public Hand.HAND_EVALUATED HandType(Hand hand)
    {
        if(isRoyalFlush(hand))
        {
            return Hand.HAND_EVALUATED.ROYAL_FLUSH;
        }
        if (isStraightFlush(hand))
        {
            return Hand.HAND_EVALUATED.STRAIGHT_FLUSH;
        }
        if (isFourOfAKind(hand))
        {
            return Hand.HAND_EVALUATED.FOUR_OF_A_KIND;
        }
        if (isFlush(hand))
        {
            return Hand.HAND_EVALUATED.FLUSH;
        }
        if (isStraight(hand))
        {
            return Hand.HAND_EVALUATED.STRAIGHT;
        }
        if (isWheelStraight(hand))
        {
            return Hand.HAND_EVALUATED.WHEEL_STRAIGHT;
        }
        if (isFullHouse(hand))
        {
            return Hand.HAND_EVALUATED.FULL_HOUSE;
        }
        if (isThreeOfAKind(hand))
        {
            return Hand.HAND_EVALUATED.THREE_OF_A_KIND;
        }
        if (isTwoPair(hand))
        {
            return Hand.HAND_EVALUATED.TWO_PAIR;
        }
        if (isPair(hand))
        {
            return Hand.HAND_EVALUATED.PAIR;
        }
        return Hand.HAND_EVALUATED.HIGH_CARD;
    }

    public bool isRoyalFlush(Hand hand)
    {
        hand.evaluatedHand = sortHandByValue(hand.evaluatedHand);
        if (isStraight(hand) && isFlush(hand) && getCardVal(hand.evaluatedHand[hand.evaluatedHand.Count - 1]) == 12)
        {
            return true;
        }
        return false;
    }

    public bool isStraightFlush(Hand hand)
    {
        hand.evaluatedHand = sortHandByValue(hand.evaluatedHand);

        //TODO:
        //Rework checking flush because currently, you can have a straight and a flush, but flush cards not belonging to that straight
        if (isFlush(hand) && isStraight(hand) || isFlush(hand) && isWheelStraight(hand))
        {
            return true;
        }
        return false;
    }

    public bool isFlush(Hand hand)
    {
        sortHandBySuit(hand.evaluatedHand);
        int counter = 0;
        Card.SUIT suit_to_check = hand.evaluatedHand[0].suit;
        for(int i = 0; i < hand.evaluatedHand.Count - 1; ++i)
        {
            if(hand.evaluatedHand[i].suit == hand.evaluatedHand[i + 1].suit)
            {
                counter++;
                if (counter == HAND_LIMIT)
                {
                    suit_to_check = hand.evaluatedHand[i].suit;
                    break;
                }
                continue;
            }
            counter = 0;
        }
        for(int i = 0; i < hand.evaluatedHand.Count; ++i)
        {
            if(hand.evaluatedHand[i].suit == suit_to_check)
            {
                hand.card_to_compare = hand.evaluatedHand[i];
            }
        }
        return counter >= 4;
    }

    public bool isStraight(Hand hand)
    {
        hand.evaluatedHand = sortHandByValue(hand.evaluatedHand);
        bool isStraight = false;
        if(getCardVal(hand.evaluatedHand[0]) == getCardVal(hand.evaluatedHand[1]) - 1 && getCardVal(hand.evaluatedHand[0]) == getCardVal(hand.evaluatedHand[2]) - 2
            && getCardVal(hand.evaluatedHand[0]) == getCardVal(hand.evaluatedHand[3]) - 3 && getCardVal(hand.evaluatedHand[0]) == getCardVal(hand.evaluatedHand[4]) - 4)
        {
            isStraight = true;
            hand.card_to_compare = hand.evaluatedHand[4];
        }
        if (hand.evaluatedHand.Count == 6)
        {
            if (getCardVal(hand.evaluatedHand[1]) == getCardVal(hand.evaluatedHand[2]) - 1 && getCardVal(hand.evaluatedHand[1]) == getCardVal(hand.evaluatedHand[3]) - 2
               && getCardVal(hand.evaluatedHand[1]) == getCardVal(hand.evaluatedHand[4]) - 3 && getCardVal(hand.evaluatedHand[1]) == getCardVal(hand.evaluatedHand[5]) - 4)
            {
                isStraight = true;
                hand.card_to_compare = hand.evaluatedHand[5];
            }
        }
        if (hand.evaluatedHand.Count == 7)
        {
            if (getCardVal(hand.evaluatedHand[2]) == getCardVal(hand.evaluatedHand[3]) - 1 && getCardVal(hand.evaluatedHand[2]) == getCardVal(hand.evaluatedHand[4]) - 2
               && getCardVal(hand.evaluatedHand[2]) == getCardVal(hand.evaluatedHand[5]) - 3 && getCardVal(hand.evaluatedHand[2]) == getCardVal(hand.evaluatedHand[6]) - 4)
            {
                isStraight = true;
                hand.card_to_compare = hand.evaluatedHand[6];
            }
        }
        return isStraight;
    }

    public bool isWheelStraight(Hand hand)
    {
        List<Card.CARD_VALUE> card_values = new List<Card.CARD_VALUE>();
        for(int i = 0; i < hand.evaluatedHand.Count; ++i)
        {
            card_values.Add(hand.evaluatedHand[i].card_value);
        }

        if(card_values.Contains(Card.CARD_VALUE.ACE) && card_values.Contains(Card.CARD_VALUE.TWO) && card_values.Contains(Card.CARD_VALUE.THREE)
           && card_values.Contains(Card.CARD_VALUE.FOUR) && card_values.Contains(Card.CARD_VALUE.FIVE))
        {
            return true;
        }
        return false;
    }

    public bool isFourOfAKind(Hand hand)
    {
        hand.evaluatedHand = sortHandByValue(hand.evaluatedHand);

        for (int i = 0; i < (hand.evaluatedHand.Count - 3); ++i)
        {
            if (getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[1 + i]) && getCardVal(hand.evaluatedHand[1 + i]) == getCardVal(hand.evaluatedHand[2 + i]) && getCardVal(hand.evaluatedHand[2 + i]) == getCardVal(hand.evaluatedHand[3 + i]))
            {
                hand.card_to_compare = hand.evaluatedHand[0 + i];
                return true;
            }
        }
        return false;
    }

    public bool isFullHouse(Hand hand)
    {
        hand.evaluatedHand = sortHandByValue(hand.evaluatedHand);

        for (int i = 0; i < (hand.evaluatedHand.Count - 4); ++i)
        {
            if(getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[i + 1]) && getCardVal(hand.evaluatedHand[i + 2]) == getCardVal(hand.evaluatedHand[i + 3]) && getCardVal(hand.evaluatedHand[i + 3]) == getCardVal(hand.evaluatedHand[i + 4]))
            {
                hand.card_to_compare = hand.evaluatedHand[0 + i];
                hand.card_to_compare_two = hand.evaluatedHand[i + 2];
                return true;
            }
            if (getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[i + 1]) && getCardVal(hand.evaluatedHand[i + 0]) == getCardVal(hand.evaluatedHand[i + 2]) && getCardVal(hand.evaluatedHand[i + 3]) == getCardVal(hand.evaluatedHand[i + 4]))
            {
                hand.card_to_compare = hand.evaluatedHand[2 + i];
                hand.card_to_compare_two = hand.evaluatedHand[i + 0];
                return true;
            }
        }
        return false;
    }

    public bool isThreeOfAKind(Hand hand)
    {
        hand.evaluatedHand = sortHandByValue(hand.evaluatedHand);

        for (int i = 0; i < (hand.evaluatedHand.Count - 2); ++i)
        {
            if (getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[1 + i]) && getCardVal(hand.evaluatedHand[1 + i]) == getCardVal(hand.evaluatedHand[2 + i]))
            {
                hand.card_to_compare = hand.evaluatedHand[0 + i];
                return true;
            }
        }
        return false;
    }

    public bool isTwoPair(Hand hand)
    {
        hand.evaluatedHand = sortHandByValue(hand.evaluatedHand);
        int pairCounter = 0;
        for(int i = 0; i < (hand.evaluatedHand.Count - 1); ++i)
        {
            if(getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[1 + i]))
            {
                if(pairCounter == 0)
                {
                    hand.card_to_compare = hand.evaluatedHand[0 + i];
                }
                if(pairCounter == 1)
                {
                    hand.card_to_compare_two = hand.evaluatedHand[0 + i];
                }
                pairCounter++;
            }
        }
        return pairCounter == 2;
    }

    public bool isPair(Hand hand)
    {
        hand.evaluatedHand = sortHandByValue(hand.evaluatedHand);
        bool isPair = false;
        for(int i = 0; i < hand.evaluatedHand.Count - 1; ++i)
        {
            if(getCardVal(hand.evaluatedHand[0 + i]) == getCardVal(hand.evaluatedHand[1 + i]))
            {
                hand.card_to_compare = hand.evaluatedHand[0 + i];
                isPair = true;
            }
        }
        return isPair;
    }

    public int CompareCardValues(Card card1, Card card2)
    {
        if((int)card1.card_value > (int)card2.card_value)
        {
            return 1;
        }
        else if((int)card2.card_value > (int)card1.card_value)
        {
            return -1;
        }
        return 0;
    }

    public int CompareKickers(Hand hand1, Hand hand2)
    {
        for(int i = hand1.evaluatedHand.Count - 1; i > HAND_SIZE - hand1.evaluatedHand.Count; --i)
        {
            int compare = CompareCardValues(hand1.evaluatedHand[i], hand2.evaluatedHand[i]);
            if(compare == 0)
            {
                continue;
            }
            return compare;
        }
        return 0;
    }

    public int compareTwoHands(Hand hand1, Hand hand2)
    {
       if(isRoyalFlush(hand1) && !isRoyalFlush(hand2))
       {
           return 1;
       }
       if(isRoyalFlush(hand2) && !isRoyalFlush(hand1))
       {
           return -1;
       }
       if(isStraightFlush(hand1) && !isStraightFlush(hand2))
       {
           return 1;
       }
       if(isStraightFlush(hand2) && !isStraightFlush(hand1))
       {
           return -1;
       }
       if (isStraightFlush(hand2) && isStraightFlush(hand1))
       {
            return CompareCardValues(hand1.card_to_compare, hand2.card_to_compare);
        }
       if(isFourOfAKind(hand1) && !isFourOfAKind(hand2))
       {
           return 1;
       }
       if(isFourOfAKind(hand2) && !isFourOfAKind(hand1))
       {
           return -1;
       }
       if(isFourOfAKind(hand1) && isFourOfAKind(hand2))
       {
            int compare = CompareCardValues(hand1.card_to_compare, hand2.card_to_compare);

            if (compare == 0)
            {
                if(getCardVal(hand1.card_to_compare) < getCardVal(hand1.evaluatedHand[hand1.evaluatedHand.Count - 1]))
                {
                    if(getCardVal(hand2.card_to_compare) < getCardVal(hand2.evaluatedHand[hand2.evaluatedHand.Count - 1]))
                    {
                        compare = CompareCardValues(hand1.evaluatedHand[hand1.evaluatedHand.Count - 1], hand2.evaluatedHand[hand2.evaluatedHand.Count - 1]);
                    }
                    else if (getCardVal(hand2.card_to_compare) == getCardVal(hand2.evaluatedHand[hand2.evaluatedHand.Count - 1]))
                    {
                        compare = CompareCardValues(hand1.evaluatedHand[hand1.evaluatedHand.Count - 1], hand2.evaluatedHand[2]);
                    }
                }
                else if(getCardVal(hand1.card_to_compare) == getCardVal(hand1.evaluatedHand[hand1.evaluatedHand.Count - 1]))
                {
                    if (getCardVal(hand2.card_to_compare) < getCardVal(hand2.evaluatedHand[hand2.evaluatedHand.Count - 1]))
                    {
                        compare = CompareCardValues(hand1.evaluatedHand[2], hand2.evaluatedHand[hand2.evaluatedHand.Count - 1]);
                    }
                    else if (getCardVal(hand2.card_to_compare) == getCardVal(hand2.evaluatedHand[hand2.evaluatedHand.Count - 1]))
                    {
                        compare = CompareCardValues(hand1.evaluatedHand[2], hand2.evaluatedHand[2]);
                    }
                }
            }
            return compare;
        }
       if(isFullHouse(hand1) && !isFullHouse(hand2))
       {
           return 1;
       }
       if(isFullHouse(hand2) && !isFullHouse(hand1))
       {
            return -1;
       }
       if(isFullHouse(hand1) && isFullHouse(hand2))
       {
            int compare = CompareCardValues(hand1.card_to_compare, hand2.card_to_compare);
            if(compare == 0)
            {
                return CompareCardValues(hand1.card_to_compare_two, hand2.card_to_compare_two);
            }
            return compare;
       }
       if(isThreeOfAKind(hand1) && !isThreeOfAKind(hand2))
       {
            return 1;
       }
       if(isThreeOfAKind(hand2) && !isThreeOfAKind(hand1))
       {
           return -1;
       }
       if(isThreeOfAKind(hand1) && isThreeOfAKind(hand2))
       {
            int compare = CompareCardValues(hand1.card_to_compare, hand2.card_to_compare);

            if (compare == 0)
            {
                List<Card> cards_to_compare_hand1 = new List<Card>();
                List<Card> cards_to_compare_hand2 = new List<Card>();
                int counter = 0;

                for(int i = hand1.evaluatedHand.Count - 1; i > 0; --i)
                {
                    if(counter == 2)
                    {
                        break;
                    }
                    if(getCardVal(hand1.evaluatedHand[i]) == getCardVal(hand1.card_to_compare))
                    {
                        continue;
                    }
                    cards_to_compare_hand1.Add(hand1.evaluatedHand[i]);
                    counter++;
                }
                counter = 0;
                for (int i = hand2.evaluatedHand.Count - 1; i > 0; --i)
                {
                    if (counter == 2)
                    {
                        break;
                    }
                    if (getCardVal(hand2.evaluatedHand[i]) == getCardVal(hand2.card_to_compare))
                    {
                        continue;
                    }
                    cards_to_compare_hand2.Add(hand2.evaluatedHand[i]);
                    counter++;
                }
                cards_to_compare_hand1 = sortHandByValue(cards_to_compare_hand1);
                cards_to_compare_hand2 = sortHandByValue(cards_to_compare_hand2);

                compare = CompareCardValues(cards_to_compare_hand1[1], cards_to_compare_hand2[1]);

                if(compare == 0)
                {
                    compare = CompareCardValues(cards_to_compare_hand1[0], cards_to_compare_hand2[0]);
                }

            }
            return compare;
        }
       if(isTwoPair(hand1) && !isTwoPair(hand2))
       {
           return 1;
       }
       if(isTwoPair(hand2) && !isTwoPair(hand1))
       {
           return -1;
       }
       if(isTwoPair(hand1) && isTwoPair(hand2))
       {
           int compare = CompareCardValues(hand1.card_to_compare, hand2.card_to_compare);
           if (compare == 0)
           {
               compare = CompareCardValues(hand1.card_to_compare_two, hand2.card_to_compare_two);

                if (compare == 0)
                {
                    List<Card> copy_hand_1 = hand1.evaluatedHand.ToList();
                    List<Card> copy_hand_2 = hand2.evaluatedHand.ToList();

                    for (int i = copy_hand_1.Count - 1; i > 0; --i)
                    {
                        if (copy_hand_1.Contains(hand1.card_to_compare) || copy_hand_1.Contains(hand1.card_to_compare_two))
                        {
                            copy_hand_1.RemoveAt(i);
                        }
                    }
                    copy_hand_1 = sortHandByValue(copy_hand_1);

                    for (int i = copy_hand_2.Count - 1; i > 0; --i)
                    {
                        if (copy_hand_2.Contains(hand2.card_to_compare) || copy_hand_2.Contains(hand2.card_to_compare_two))
                        {
                            copy_hand_2.RemoveAt(i);
                        }
                    }
                    copy_hand_2 = sortHandByValue(copy_hand_2);

                    compare = CompareCardValues(copy_hand_1[copy_hand_1.Count - 1], copy_hand_2[copy_hand_2.Count - 1]);
                }
           }
            return compare;
        }
       if(isPair(hand1) && !isPair(hand2))
       {
           return 1;
       }
       if(isPair(hand2) && !isPair(hand1))
       {
           return -1;
       }
       if(isPair(hand1) && isPair(hand2))
       {
            int compare = CompareCardValues(hand1.card_to_compare, hand2.card_to_compare);

            if(compare == 0)
            {
                List<Card> copy_hand_1 = hand1.evaluatedHand.ToList();
                List<Card> copy_hand_2 = hand2.evaluatedHand.ToList();

                for (int i = copy_hand_1.Count - 1; i > 0; --i)
                {
                    if (copy_hand_1.Contains(hand1.card_to_compare))
                    {
                        copy_hand_1.RemoveAt(i);
                    }
                }
                copy_hand_1 = sortHandByValue(copy_hand_1);

                for (int i = copy_hand_2.Count - 1; i > 0; --i)
                {
                    if (copy_hand_2.Contains(hand2.card_to_compare))
                    {
                        copy_hand_2.RemoveAt(i);
                    }
                }
                copy_hand_2 = sortHandByValue(copy_hand_2);

                for(int i = copy_hand_1.Count - 1; i > 1; --i)
                {
                    compare = CompareCardValues(copy_hand_1[i], copy_hand_2[i]);
                    if(compare != 0)
                    {
                        break;
                    }
                }
            }
            return compare;
       }
        return CompareKickers(hand1, hand2);
    }
}
