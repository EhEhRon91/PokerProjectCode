using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagement : MonoBehaviour
{
    public Player player;
    public Player enemy1;
    public Player enemy2;
    public Player enemy3;

    public Pot pot;

    public static int turn_counter = 0;
    public static int card_counter = 0;
    public static int round_counter = 0;
    public static int player_won_count = 1;

    public const float TURN_LENGTH = 1.0f;
    public float turn_timer = 0.0f;

    private void Start()
    {
        player.isStarting = true;
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

    public void EnemyBet(Player player, int amount)
    {
        player.isMyTurn = false;
        player.doneTurn = true;
        pot.amount += amount;
        pot.min_amount_to_bet = amount;
        pot.turn_amount = 0;
        player.min_amount_to_bet = amount;
        player.chips -= amount;
        turn_counter++;
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
        pot.turn_amount = 0;
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
