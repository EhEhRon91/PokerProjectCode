using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public Chips pot;
    public Player user;
    public Player enemy1;
    public Player enemy2;
    public Player enemy3;

    public int SMALL_BLIND = 100;
    public int BIG_BLIND = 200;

    public void setBlinds(int blind_counter)
    {
        SMALL_BLIND *= blind_counter;
        BIG_BLIND *= blind_counter;
    }

    // Start is called before the first frame update
    void Start()
    {
        pot = new Chips(0);
    }

    public void AddToPot(int amount, Player player)
    {
        pot.amount = player.Bet(amount);
    }
}
