using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBlocksPlayerMovement : MonoBehaviour
{
    public PlayerController playerController;

    private void OnEnable()
    {
        if (playerController != null)
            playerController.SetPlayerMovement(false);
    }

    private void OnDisable()
    {
        if (playerController != null)
            playerController.SetPlayerMovement(true);
    }
}
