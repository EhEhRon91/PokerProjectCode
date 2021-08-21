using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public Player player;
    public Player enemy1;
    public Player enemy2;
    public Player enemy3;
    public Hand playerHand;
    public Hand enemy1Hand;
    public Hand enemy2Hand;
    public Hand enemy3Hand;
    public TurnManagement turnManager;
    public Pot pot;
    public int amount_to_bet;

    public Hand.HAND_EVALUATED getHandValue(Hand hand)
    {
        return hand.hand_evaluated;
    }

    public bool Fold(Hand hand, int flopCount)
    {
        if (hand.hand_evaluated >= Hand.HAND_EVALUATED.PAIR)
        {
            return false;
        }
        else
        {
            if (hand.card_to_compare.card_value > Card.CARD_VALUE.KING)
            {
                return false;
            }
        }
        return true;
    }

    public bool Stay(Hand hand, int flopCount)
    {
        if (hand.hand_evaluated >= Hand.HAND_EVALUATED.PAIR)
        {
            return false;
        }
        else
        {
            if (hand.card_to_compare.card_value > Card.CARD_VALUE.KING)
            {
                return false;
            }
        }
        return true;
    }

    public int WhatToBet(Player player, Hand hand, int flopCount)
    {
        if(hand.hand_evaluated >= Hand.HAND_EVALUATED.FULL_HOUSE)
        {
            return player.chips;
        }
        else if(hand.hand_evaluated == Hand.HAND_EVALUATED.THREE_OF_A_KIND)
        {
            return (int)(player.chips * UnityEngine.Random.Range(0.5f, 0.8f));
        }
        else if(hand.hand_evaluated == Hand.HAND_EVALUATED.TWO_PAIR)
        {
            if(hand.card_to_compare_two.card_value == Card.CARD_VALUE.ACE)
            {
                return (int)(player.chips * UnityEngine.Random.Range(0.3f, 0.5f));
            }
            else if(hand.card_to_compare_two.card_value >= Card.CARD_VALUE.TEN)
            {
                return (int)(player.chips * UnityEngine.Random.Range(0.2f, 0.4f));
            }
            else
            {
                return (int)(player.chips * UnityEngine.Random.Range(0.1f, 0.2f));
            }
        }
        else if(hand.hand_evaluated == Hand.HAND_EVALUATED.PAIR)
        {
            if (hand.card_to_compare.card_value == Card.CARD_VALUE.ACE)
            {
                return (int)(player.chips * UnityEngine.Random.Range(0.05f, 0.1f));
            }
            else if (hand.card_to_compare.card_value >= Card.CARD_VALUE.TEN)
            {
                return (int)(player.chips * UnityEngine.Random.Range(0.04f, 0.08f));
            }
            else
            {
                return (int)(player.chips * UnityEngine.Random.Range(0.02f, 0.05f));
            }
        }
        else
        {
            if (hand.card_to_compare.card_value == Card.CARD_VALUE.ACE)
            {
                return (int)(player.chips * UnityEngine.Random.Range(0.01f, 0.02f));
            }
            else if (hand.card_to_compare.card_value >= Card.CARD_VALUE.TEN)
            {
                return (int)(player.chips * UnityEngine.Random.Range(0.005f, 0.01f));
            }
            else
            {
                return (int)(player.chips * UnityEngine.Random.Range(0.0005f, 0.001f));
            }
        }
    }
}
