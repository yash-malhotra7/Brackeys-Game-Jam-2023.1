using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGhostInScene = false;

    public void Shoot(GameObject arrow)
    {

    }

    private void Dash(float direction, bool Grounded, Rigidbody2D rb, float dashSpeed, float dashDuration)
    {
        if (!Grounded)
        {
            return; // Can't dash while in the air
        }

        rb.velocity = new Vector2(direction * dashSpeed, rb.velocity.y);

        float dashTime = 0f;
        while (dashTime < dashDuration)
        {
            dashTime += Time.deltaTime;
            if (!Grounded)
            {
                break; // Stop dashing if character falls off the ground
            }
        }

        rb.velocity = new Vector2(0f, rb.velocity.y);
    }
}


