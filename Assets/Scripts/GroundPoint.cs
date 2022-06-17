using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoint : MonoBehaviour
{
    public PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            playerController.SetCanDoubleJump(false);
            playerController.SetIsJumping(false);
            playerController.SetIsDoubleJumping(false);
        }
    }
}
