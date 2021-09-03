using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    public Player player;

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Button>().interactable = player.isMyTurn;
    }
}
