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

    public void FoldOrBet(Hand hand)
    {
        if(getHandValue(hand) == Hand.HAND_EVALUATED.HIGH_CARD)
        {

        }
    }
}
