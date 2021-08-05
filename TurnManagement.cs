using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagement : MonoBehaviour
{
    public Player user;
    public Player enemy1;
    public Player enemy2;
    public Player enemy3;

    public Pot pot;

    public static int turn_counter = 0;

    public const float TURN_LENGTH = 1.0f;
    public float turn_timer = 0.0f;
    private void Start()
    {
        Turn(user);
    }

    public void Fold(Player player)
    {
        player.hasFolded = true;
        player.isMyTurn = false;
    }

    public void PlayerBet()
    {
        user.isMyTurn = false;
    }

    public void AddToBet()
    {
        if(user.chips <= 0)
        {
            return;
        }
        pot.AddToPotPlayer(Pot.SMALL_BLIND);
    }

    public void RemoveFromBet()
    {
        if(pot.amount <= 0)
        {
            return;
        }
        pot.AddToPotPlayer(-Pot.SMALL_BLIND);
    }

    public void Turn(Player player)
    {
        turn_counter++;
        player.isMyTurn = true;
        pot.setBlinds((int)Math.Ceiling(turn_counter / 4.0f));
    }
}
