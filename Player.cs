using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Chips chips;
    private int START_AMOUNT = 1000000;
    public bool isMyTurn = false;
    public bool hasFolded = false;
    // Start is called before the first frame update
    void Start()
    {
        chips = new Chips(START_AMOUNT);
    }

    public int Bet(int amount)
    {
        return amount;
    }

    public void Fold()
    {
        hasFolded = true;
    }
}
