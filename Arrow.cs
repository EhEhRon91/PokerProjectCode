using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private const float ROTATION_DEGREES = 90.0f;
    private Vector3 rotation = new Vector3(0.0f, 0.0f, 270.0f);
    public Player player;
    public TurnManagement turnManager;
    private bool hasRotated = false;
    private float rotation_value;

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

                if (TurnManagement.turn_counter % 4 == 0)
                {
                    player.isMyTurn = true;
                }

                TurnManagement.turn_counter++;
            }
        }
    }
}
