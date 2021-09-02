using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    private const float ROTATION_DEGREES = 90.0f;
    private const float TIME_TO_DISPLAY_RESULTS = 5.0f;
    private float timer = 0.0f;
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
    public Pot pot;
    public Flop flop;
    public Text WinningText;
    private int turn;
    public Enemy_AI AI;

    public void RotateArrow(int turn_counter)
    {
        if (turn_counter == 0)
        {
            if (player.isStarting)
            {
                turn = 1;
                gameObject.transform.rotation = Quaternion.AngleAxis(-ROTATION_DEGREES * (TurnManagement.turn_counter + 1), Vector3.forward);
            }
            else if (enemy1.isStarting)
            {
                turn = 2;
                gameObject.transform.rotation = Quaternion.AngleAxis(-ROTATION_DEGREES * (TurnManagement.turn_counter + 2), Vector3.forward);
            }
            else if (enemy2.isStarting)
            {
                turn = 3;
                gameObject.transform.rotation = Quaternion.AngleAxis(-ROTATION_DEGREES * (TurnManagement.turn_counter + 3), Vector3.forward);
            }
            else if (enemy3.isStarting)
            {
                turn = 4;
                gameObject.transform.rotation = Quaternion.AngleAxis(-ROTATION_DEGREES * (TurnManagement.turn_counter + 4), Vector3.forward);
            }
        }
        turnManager.turn_timer += Time.deltaTime;

        if (turnManager.turn_timer >= TurnManagement.TURN_LENGTH)
        {
            if (playerHand.evaluatedHand.Count != 0)
            {
                int compare = evaluator.compareTwoHands(playerHand, enemy1Hand);
                int compare2 = evaluator.compareTwoHands(enemy2Hand, enemy3Hand);
            }
            gameObject.transform.rotation = Quaternion.AngleAxis(-ROTATION_DEGREES * (turn_counter + turn), Vector3.forward);
            turnManager.turn_timer = 0.0f;

            if (turn_counter % 4 == 0 || turn_counter == 0)
            {
                if (turn == 1)
                {
                    player.isMyTurn = true;
                    enemy1.isMyTurn = false;
                    enemy2.isMyTurn = false;
                    enemy3.isMyTurn = false;
                }
                else if(turn == 2)
                {
                    player.isMyTurn = false;
                    enemy1.isMyTurn = true;
                    enemy2.isMyTurn = false;
                    enemy3.isMyTurn = false;
                    turnManager.EnemyBet(enemy1, AI.WhatToBet(enemy1, enemy1Hand, 0));
                }
                else if(turn == 3)
                {
                    player.isMyTurn = false;
                    enemy1.isMyTurn = false;
                    enemy2.isMyTurn = true;
                    enemy3.isMyTurn = false;
                    turnManager.EnemyBet(enemy2, AI.WhatToBet(enemy2, enemy2Hand, 0));
                }
                else if(turn == 4)
                {
                    player.isMyTurn = false;
                    enemy1.isMyTurn = false;
                    enemy2.isMyTurn = false;
                    enemy3.isMyTurn = true;
                    turnManager.EnemyBet(enemy3, AI.WhatToBet(enemy3, enemy3Hand, 0));
                }
            }
            else if (turn_counter % 4 == 1)
            {
                if (turn == 1)
                {
                    player.isMyTurn = false;
                    enemy1.isMyTurn = true;
                    turnManager.EnemyBet(enemy1, AI.WhatToBet(enemy1, enemy1Hand, 0));
                }
                else if (turn == 2)
                {
                    enemy1.isMyTurn = false;
                    enemy2.isMyTurn = true;
                    turnManager.EnemyBet(enemy2, AI.WhatToBet(enemy2, enemy2Hand, 0));
                }
                else if (turn == 3)
                {
                    enemy2.isMyTurn = false;
                    enemy3.isMyTurn = true;
                    turnManager.EnemyBet(enemy3, AI.WhatToBet(enemy3, enemy3Hand, 0));
                }
                else if (turn == 4)
                {
                    player.isMyTurn = true;
                    enemy3.isMyTurn = false;
                }
            }
            else if (turn_counter % 4 == 2)
            {
                if (turn == 1)
                {
                    enemy1.isMyTurn = false;
                    enemy2.isMyTurn = true;
                    turnManager.EnemyBet(enemy2, AI.WhatToBet(enemy2, enemy2Hand, 0));
                }
                else if (turn == 2)
                {
                    enemy2.isMyTurn = false;
                    enemy3.isMyTurn = true;
                    turnManager.EnemyBet(enemy3, AI.WhatToBet(enemy3, enemy3Hand, 0));
                }
                else if (turn == 3)
                {
                    player.isMyTurn = true;
                    enemy3.isMyTurn = false;
                }
                else if (turn == 4)
                {
                    player.isMyTurn = false;
                    enemy1.isMyTurn = true;
                    turnManager.EnemyBet(enemy1, AI.WhatToBet(enemy1, enemy1Hand, 0));
                }
            }
            else if (turn_counter % 4 == 3)
            {
                if (turn == 1)
                {
                    enemy2.isMyTurn = false;
                    enemy3.isMyTurn = true;
                    turnManager.EnemyBet(enemy3, AI.WhatToBet(enemy3, enemy3Hand, 0));
                }
                else if (turn == 2)
                {
                    player.isMyTurn = true;
                    enemy3.isMyTurn = false;
                }
                else if (turn == 3)
                {
                    player.isMyTurn = false;
                    enemy1.isMyTurn = true;
                    turnManager.EnemyBet(enemy1, AI.WhatToBet(enemy1, enemy1Hand, 0));
                }
                else if (turn == 4)
                {
                    enemy1.isMyTurn = false;
                    enemy2.isMyTurn = true;
                    turnManager.EnemyBet(enemy2, AI.WhatToBet(enemy2, enemy2Hand, 0));
                }
            }
        }
    }

    public void FinishRound()
    {
        int compare = evaluator.compareTwoHands(playerHand, enemy1Hand);
        int compare2 = evaluator.compareTwoHands(enemy2Hand, enemy3Hand);
        int compare3 = 0;

        if (compare == 1 && compare2 == 1)
        {
            compare3 = evaluator.compareTwoHands(playerHand, enemy2Hand);

            if (compare3 == 1)
            {
                player.hasWon = true;
            }
            else if (compare3 == -1)
            {
                enemy2.hasWon = true;
            }
            else if (compare3 == 0)
            {
                player.hasWon = true;
                enemy2.hasWon = true;
                TurnManagement.player_won_count = 2;
            }
        }
        else if (compare == 1 && compare2 == -1)
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
        else if (compare == -1 && compare2 == 1)
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
        else if (compare == -1 && compare2 == -1)
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
        else if (compare == 0)
        {
            if (compare2 == 1)
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
            else if (compare2 == -1)
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
            else if (compare2 == 0)
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
        else if (compare2 == 0)
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
    // Update is called once per frame
    void Update()
    {
        RotateArrow(TurnManagement.turn_counter);

        if (turnManager.DoneTurn() && ((player.isMyTurn && player.isStarting) || (enemy1.isStarting) || (enemy2.isStarting) || (enemy3.isStarting)))
        {
            if (TurnManagement.card_counter == 2)
            {
                FinishRound();
            }
            if (timer == 0.0f)
            {
                if (player.hasWon == true)
                {
                    playerHand.hand_evaluated = evaluator.HandType(playerHand);
                    WinningText.text += "Player 1 has Won with " + evaluator.HandString(playerHand);
                }
                if (enemy1.hasWon == true)
                {
                    enemy1Hand.hand_evaluated = evaluator.HandType(enemy1Hand);
                    WinningText.text += "Player 2 has Won with " + evaluator.HandString(enemy1Hand);
                }
                if (enemy2.hasWon == true)
                {
                    enemy2Hand.hand_evaluated = evaluator.HandType(enemy2Hand);
                    WinningText.text += "Player 3 has Won with " + evaluator.HandString(enemy2Hand);
                }
                if (enemy3.hasWon == true)
                {
                    enemy3Hand.hand_evaluated = evaluator.HandType(enemy3Hand);
                    WinningText.text += "Player 4 has Won with " + evaluator.HandString(enemy3Hand);
                }
            }
            if (player.hasWon == true || enemy1.hasWon == true || enemy2.hasWon == true || enemy3.hasWon == true)
            {
                timer += Time.deltaTime;
                if (timer >= TIME_TO_DISPLAY_RESULTS)
                {
                    turnManager.ResetRound();
                    timer = 0.0f;
                    WinningText.text = "";
                }
            }
            if (flop.flop.Count < 5 && player.doneTurn == true && enemy1.doneTurn == true && enemy2.doneTurn == true && enemy3.doneTurn == true)
            {
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
}
