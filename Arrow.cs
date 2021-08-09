using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    private const float ROTATION_DEGREES = 90.0f;
    private Vector3 rotation = new Vector3(0.0f, 0.0f, 270.0f);
    public Player player;
    public Player enemy1;
    public Player enemy2;
    public Player enemy3;
    public Hand playerHand;
    public Hand enemy1Hand;
    public Hand enemy2Hand;
    public Hand enemy3Hand;
    public TurnManagement turnManager;
    public Evaluator evaluator;
    private bool hasRotated = false;
    private float rotation_value;
    public Pot pot;
    public Flop flop;
    public Text WinningText;

    // Update is called once per frame
    void Update()
    {
        if(!player.isMyTurn)
        {
            if (hasRotated == false)
            {
                hasRotated = true;
            }

            turnManager.turn_timer += Time.deltaTime;

            if(turnManager.turn_timer >= TurnManagement.TURN_LENGTH)
            {
                gameObject.transform.rotation = Quaternion.AngleAxis(-ROTATION_DEGREES * (TurnManagement.turn_counter + 1), Vector3.forward);
                turnManager.turn_timer = 0.0f;
                hasRotated = false;

                if (TurnManagement.turn_counter % 4 == 0 || TurnManagement.turn_counter == 0)
                {
                    player.isMyTurn = true;
                    enemy1.isMyTurn = false;
                    enemy2.isMyTurn = false;
                    enemy3.isMyTurn = false;
                }
                else if(TurnManagement.turn_counter % 4 == 3)
                {
                    player.isMyTurn = false;
                    enemy1.isMyTurn = true;
                    enemy2.isMyTurn = false;
                    enemy3.isMyTurn = false;
                    turnManager.EnemyBet(enemy1, pot.min_amount_to_bet);
                }
                else if(TurnManagement.turn_counter % 4 == 2)
                {
                    player.isMyTurn = false;
                    enemy1.isMyTurn = false;
                    enemy2.isMyTurn = true;
                    enemy3.isMyTurn = false;
                    turnManager.EnemyBet(enemy2, pot.min_amount_to_bet);
                }
                else if(TurnManagement.turn_counter % 4 == 1)
                {
                    player.isMyTurn = false;
                    enemy1.isMyTurn = false;
                    enemy2.isMyTurn = false;
                    enemy3.isMyTurn = true;
                    turnManager.EnemyBet(enemy3, pot.min_amount_to_bet);
                }
            }
        }
        if (turnManager.DoneTurn() && player.isMyTurn)
        {
            if (TurnManagement.card_counter == 2)
            {
                int compare = evaluator.compareTwoHands(playerHand, enemy1Hand);
                int compare2 = evaluator.compareTwoHands(enemy2Hand, enemy3Hand);
                int compare3 = 0;

                if(compare == 1 && compare2 == 1)
                {
                    compare3 = evaluator.compareTwoHands(playerHand, enemy2Hand);

                    if(compare3 == 1)
                    {
                        player.hasWon = true;
                    }
                    else if(compare3 == -1)
                    {
                        enemy2.hasWon = true;
                    }
                    else if(compare3 == 0)
                    {
                        player.hasWon = true;
                        enemy2.hasWon = true;
                        TurnManagement.player_won_count = 2;
                    }
                }
                else if(compare == 1 && compare2 == -1)
                {
                    compare3 = evaluator.compareTwoHands(playerHand, enemy3Hand);

                    if (compare3 == 1)
                    {
                        player.hasWon = true;
                    }
                    else if (compare3 == -1)
                    {
                        enemy3.hasWon = true;
                    }
                    else if (compare3 == 0)
                    {
                        player.hasWon = true;
                        enemy3.hasWon = true;
                        TurnManagement.player_won_count = 2;
                    }
                }
                else if(compare == -1 && compare2 == 1)
                {
                    compare3 = evaluator.compareTwoHands(enemy1Hand, enemy2Hand);

                    if (compare3 == 1)
                    {
                        enemy1.hasWon = true;
                    }
                    else if (compare3 == -1)
                    {
                        enemy2.hasWon = true;
                    }
                    else if (compare3 == 0)
                    {
                        enemy1.hasWon = true;
                        enemy2.hasWon = true;
                        TurnManagement.player_won_count = 2;
                    }
                }
                else if(compare == -1 && compare2 == -1)
                {
                    compare3 = evaluator.compareTwoHands(enemy1Hand, enemy3Hand);

                    if (compare3 == 1)
                    {
                        enemy1.hasWon = true;
                    }
                    else if (compare3 == -1)
                    {
                        enemy3.hasWon = true;
                    }
                    else if (compare3 == 0)
                    {
                        enemy1.hasWon = true;
                        enemy3.hasWon = true;
                        TurnManagement.player_won_count = 2;
                    }
                }
                else if(compare == 0)
                {
                    if(compare2 == 1)
                    {
                        compare3 = evaluator.compareTwoHands(playerHand, enemy2Hand);

                        if (compare3 == 1)
                        {
                            player.hasWon = true;
                            enemy1.hasWon = true;
                            TurnManagement.player_won_count = 2;
                        }
                        else if (compare3 == -1)
                        {
                            enemy2.hasWon = true;
                        }
                        else if (compare3 == 0)
                        {
                            player.hasWon = true;
                            enemy1.hasWon = true;
                            enemy2.hasWon = true;
                            TurnManagement.player_won_count = 3;
                        }
                    }
                    else if(compare2 == -1)
                    {
                        compare3 = evaluator.compareTwoHands(playerHand, enemy3Hand);

                        if (compare3 == 1)
                        {
                            player.hasWon = true;
                            enemy1.hasWon = true;
                            TurnManagement.player_won_count = 2;
                        }
                        else if (compare3 == -1)
                        {
                            enemy3.hasWon = true;
                        }
                        else if (compare3 == 0)
                        {
                            player.hasWon = true;
                            enemy1.hasWon = true;
                            enemy3.hasWon = true;
                            TurnManagement.player_won_count = 3;
                        }
                    }
                    else if(compare2 == 0)
                    {
                        compare3 = evaluator.compareTwoHands(playerHand, enemy2Hand);

                        if (compare3 == 1)
                        {
                            player.hasWon = true;
                            enemy1.hasWon = true;
                            TurnManagement.player_won_count = 2;
                        }
                        else if (compare3 == -1)
                        {
                            enemy2.hasWon = true;
                            enemy3.hasWon = true;
                            TurnManagement.player_won_count = 2;
                        }
                        else if (compare3 == 0)
                        {
                            player.hasWon = true;
                            enemy1.hasWon = true;
                            enemy2.hasWon = true;
                            enemy3.hasWon = true;
                            TurnManagement.player_won_count = 4;
                        }
                    }
                }
                else if(compare2 == 0)
                {
                    if (compare == 1)
                    {
                        compare3 = evaluator.compareTwoHands(playerHand, enemy2Hand);

                        if (compare3 == 1)
                        {
                            player.hasWon = true;
                        }
                        else if (compare3 == -1)
                        {
                            enemy2.hasWon = true;
                            enemy3.hasWon = true;
                            TurnManagement.player_won_count = 2;
                        }
                        else if (compare3 == 0)
                        {
                            player.hasWon = true;
                            enemy1.hasWon = true;
                            enemy2.hasWon = true;
                            TurnManagement.player_won_count = 3;
                        }
                    }
                    else if (compare == -1)
                    {
                        compare3 = evaluator.compareTwoHands(enemy1Hand, enemy2Hand);

                        if (compare3 == 1)
                        {
                            enemy1.hasWon = true;
                        }
                        else if (compare3 == -1)
                        {
                            enemy2.hasWon = true;
                            enemy3.hasWon = true;
                            TurnManagement.player_won_count = 2;
                        }
                        else if (compare3 == 0)
                        {
                            enemy1.hasWon = true;
                            enemy2.hasWon = true;
                            enemy3.hasWon = true;
                            TurnManagement.player_won_count = 3;
                        }
                    }
                    else if (compare == 0)
                    {
                        compare3 = evaluator.compareTwoHands(playerHand, enemy2Hand);

                        if (compare3 == 1)
                        {
                            player.hasWon = true;
                            enemy1.hasWon = true;
                            TurnManagement.player_won_count = 2;
                        }
                        else if (compare3 == -1)
                        {
                            enemy2.hasWon = true;
                            enemy3.hasWon = true;
                            TurnManagement.player_won_count = 2;
                        }
                        else if (compare3 == 0)
                        {
                            player.hasWon = true;
                            enemy1.hasWon = true;
                            enemy2.hasWon = true;
                            enemy3.hasWon = true;
                            TurnManagement.player_won_count = 4;
                        }
                    }
                }
            }

            if(player.hasWon == true)
            {
                WinningText.text += "Player 1 has Won with " + evaluator.HandString(playerHand);
            }
            if(enemy1.hasWon == true)
            {
                WinningText.text += "Player 2 has Won with " + evaluator.HandString(enemy1Hand);
            }
            if(enemy2.hasWon == true)
            {
                WinningText.text += "Player 3 has Won with " + evaluator.HandString(enemy2Hand);
            }
            if(enemy3.hasWon == true)
            {
                WinningText.text += "Player 4 has Won with " + evaluator.HandString(enemy3Hand);
            }

            TurnManagement.card_counter++;
            turnManager.ResetTurn(player);
            turnManager.ResetTurn(enemy1);
            turnManager.ResetTurn(enemy2);
            turnManager.ResetTurn(enemy3);
            flop.DealAnotherCard();
            playerHand.AddToHandFromFlop();
            enemy1Hand.AddToHandFromFlop();
            enemy2Hand.AddToHandFromFlop();
            enemy3Hand.AddToHandFromFlop();
        }
    }
}
