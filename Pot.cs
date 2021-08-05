using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public Player player;
    public Player enemy1;
    public Player enemy2;
    public Player enemy3;

    public int amount = 0;

    public static int SMALL_BLIND = 100;
    public static int BIG_BLIND = 200;

    public void setBlinds(int blind_counter)
    {
        SMALL_BLIND *= blind_counter;
        BIG_BLIND *= blind_counter;
    }

    // Start is called before the first frame update
    void Start()
    {
        amount = 0;
    }

    public void AddToPotPlayer(int amount_added)
    {
        amount += amount_added;
        player.chips -= amount_added;
    }

    public void ResetPot()
    {
        amount = 0;
    }
}
