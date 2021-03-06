using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagement : MonoBehaviour
{
    public Player player;
    public Player enemy1;
    public Player enemy2;
    public Player enemy3;

    public Enemy_AI AI;

    public Hand playerHand;
    public Hand enemy1Hand;
    public Hand enemy2Hand;
    public Hand enemy3Hand;

    public Pot pot;

    public Flop flop;
    public Deck deck;

    public static int turn_counter = 0;
    public static int card_counter = 0;
    public static int round_counter = 0;
    public static int player_won_count = 1;

    public const float TURN_LENGTH = 1.0f;
    public float turn_timer = 0.0f;

    public bool doneChecking = false;

    private void Start()
    {
        player.isStarting = true;
    }

    public void ResetRound()
    {
        round_counter++;

        if(player.hasWon == true)
        {
            player.chips += pot.amount / player_won_count;
        }
        if(enemy1.hasWon == true)
        {
            enemy1.chips += pot.amount / player_won_count;
        }
        if(enemy2.hasWon == true)
        {
            enemy2.chips += pot.amount / player_won_count;
        }
        if(enemy3.hasWon == true)
        {
            enemy3.chips += pot.amount / player_won_count;
        }

        player.hasWon = false;
        player.doneTurn = false;
        player.hasFolded = false;
        player.isMyTurn = false;
        enemy1.hasWon = false;
        enemy1.hasFolded = false;
        enemy1.doneTurn = false;
        enemy1.isMyTurn = false;
        enemy2.hasWon = false;
        enemy2.hasFolded = false;
        enemy2.doneTurn = false;
        enemy2.isMyTurn = false;
        enemy3.hasWon = false;
        enemy3.hasFolded = false;
        enemy3.doneTurn = false;
        enemy3.isMyTurn = false;

        playerHand.hand.Clear();
        playerHand.evaluatedHand.Clear();
        enemy1Hand.hand.Clear();
        enemy1Hand.evaluatedHand.Clear();
        enemy2Hand.hand.Clear();
        enemy2Hand.evaluatedHand.Clear();
        enemy3Hand.hand.Clear();
        enemy3Hand.evaluatedHand.Clear();

        flop.flop.Clear();

        Deck.ShuffleDeck(10000);
        playerHand.PositionHand(Deck.deck, "Deck");
        Hand.deal_counter = 0;
        card_counter = 0;

        playerHand.DealHand();
        enemy1Hand.DealHand();
        enemy2Hand.DealHand();
        enemy3Hand.DealHand();
        flop.DealInitial();
        playerHand.PositionHand();
        enemy1Hand.PositionHand();
        enemy2Hand.PositionHand();
        enemy3Hand.PositionHand();
        flop.PositionCards();
        doneChecking = true;
        pot.setBlinds();

        pot.amount = 0;
        turn_counter = 0;
        player_won_count = 1;
        player.min_amount_to_bet = Pot.SMALL_BLIND;
        enemy1.min_amount_to_bet = Pot.SMALL_BLIND;
        enemy2.min_amount_to_bet = Pot.SMALL_BLIND;
        enemy3.min_amount_to_bet = Pot.SMALL_BLIND;
        pot.min_amount_to_bet = Pot.SMALL_BLIND;

        if(player.isStarting)
        {
            player.isStarting = false;
            enemy1.isStarting = true;
            enemy1.isMyTurn = true;
        }
        else if(enemy1.isStarting)
        {
            enemy1.isStarting = false;
            enemy2.isStarting = true;
            enemy2.isMyTurn = true;
        }
        else if(enemy2.isStarting)
        {
            enemy2.isStarting = false;
            enemy3.isStarting = true;
            enemy3.isMyTurn = true;
        }
        else if(enemy3.isStarting)
        {
            enemy3.isStarting = false;
            player.isStarting = true;
            player.isMyTurn = true;
        }
    }

    public bool DoneTurn()
    {
        return player.doneTurn && enemy1.doneTurn && enemy2.doneTurn && enemy3.doneTurn;
    }

    public void ResetTurn(Player player)
    {
        if(player.hasFolded)
        {
            return;
        }
        player.doneTurn = false;
    }

    public void Fold(Player player)
    {
        player.hasFolded = true;
        player.isMyTurn = false;
        player.doneTurn = true;
    }

    public void EnemyBet(Player player, Hand hand, int amount)
    {
        player.doneTurn = true;
        turn_counter++;
        if(amount < pot.turn_amount)
        {
            player.hasFolded = AI.Fold(hand, 0);
            return;
        }
        if (amount < pot.min_amount_to_bet)
        {
            player.hasFolded = true;
            return;
        }
        if (AI.Stay(hand, 0) || player.hasFolded)
            return;

        pot.amount += amount;
        pot.min_amount_to_bet = amount;
        pot.turn_amount = 0;
        player.min_amount_to_bet = amount;
        player.chips -= amount;
    }

    public void PlayerStay()
    {
        player.isMyTurn = false;
        player.doneTurn = true;
        turn_counter++;
    }
    public void PlayerBet()
    {
        player.isMyTurn = false;
        player.doneTurn = true;
        pot.amount += pot.turn_amount;
        pot.min_amount_to_bet = pot.turn_amount;
        player.min_amount_to_bet = pot.min_amount_to_bet;
        turn_counter++;
    }

    public void AddToBet()
    {
        if(player.chips <= 0)
        {
            return;
        }
        pot.AddToPotPlayer(Pot.SMALL_BLIND);
    }

    public void RemoveFromBet()
    {
        if(pot.turn_amount <= pot.amount)
        {
            return;
        }
        pot.AddToPotPlayer(-Pot.SMALL_BLIND);
    }
}
