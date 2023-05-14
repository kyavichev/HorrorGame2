using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlsManager : MonoBehaviour
{
    protected PlayerController playerController;


    public void OnForwardButtonPressed()
    {
        if(playerController == null)
        {
            FindPlayerController();
        }

        if(playerController)
        {
            playerController.MoveForward();
        }
    }


    public void OnBackwardsButtonPressed()
    {
        if (playerController == null)
        {
            FindPlayerController();
        }

        if (playerController)
        {
            playerController.MoveBack();
        }
    }


    public void OnRotateLeftButtonPressed()
    {
        if (playerController == null)
        {
            FindPlayerController();
        }

        if (playerController)
        {
            playerController.RotateLeft();
        }
    }

    public void OnRotateRightButtonPressed()
    {
        if (playerController == null)
        {
            FindPlayerController();
        }

        if (playerController)
        {
            playerController.RotateRight();
        }
    }


    public void OnInventoryButtonPressed()
    {
        GameManager.GetInstance().ToggleInventory();
    }


    protected void FindPlayerController()
    {
        if (GameManager.GetInstance())
        {
            GameObject hero = GameManager.GetInstance().hero;
            if (hero)
            {
                playerController = hero.GetComponent<PlayerController>();
            }
        }
    }
}
