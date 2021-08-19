using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int chips;
    private int START_AMOUNT = 1000000;
    public bool isMyTurn = false;
    public bool hasFolded = false;
    public bool doneTurn = false;
    public bool isStarting = false;
    public bool hasWon = false;
    public int min_amount_to_bet;
    public Pot pot;
    // Start is called before the first frame update
    void Start()
    {
        min_amount_to_bet = Pot.SMALL_BLIND;
        chips = START_AMOUNT;
    }
}
