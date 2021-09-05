using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public Player player;
    public Player enemy1;
    public Player enemy2;
    public Player enemy3;

    public int turn_amount = 0;
    public int amount = 0;

    public int min_amount_to_bet = SMALL_BLIND;

    public static int SMALL_BLIND = 100;
    public static int BIG_BLIND = 200;

    public void setBlinds()
    {
        SMALL_BLIND += 200;
        BIG_BLIND += 200;
    }

    // Start is called before the first frame update
    void Start()
    {
        amount = 0;
        turn_amount = 0;
    }

    public void SetBaseAmount()
    {
        min_amount_to_bet = SMALL_BLIND;
    }

    public void AddToPotPlayer(int amount_added)
    {
        turn_amount += amount_added;
        player.chips -= amount_added;
    }

    public void ResetPot()
    {
        amount = 0;
    }
}
