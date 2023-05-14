using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected PlayerController playerController;


    // Update is called once per frame
    void Update()
    {
        if(playerController == null)
        {
            FindPlayerController();
        }

        if (playerController)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                GameManager.GetInstance().ToggleInventory();
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerController.MoveForward();
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerController.MoveBack();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerController.RotateLeft();
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerController.RotateRight();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                playerController.Attack();
            }
        }

        if (GameManager.GetInstance().state == GameManager.GameState.Inventory)
        {
            InventoryUIController inventoryController = GameManager.GetInstance().gameUI.inventoryPanel;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                inventoryController.MoveSelection(new Vector2Int(0, -1));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                inventoryController.MoveSelection(new Vector2Int(0, 1));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                inventoryController.MoveSelection(new Vector2Int(1, 0));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                inventoryController.MoveSelection(new Vector2Int(-1, 0));
            }
        }
    }

    protected void FindPlayerController()
    {
        if(GameManager.GetInstance())
        {
            GameObject hero = GameManager.GetInstance().hero;
            if(hero)
            {
                playerController = hero.GetComponent<PlayerController>();
            }
        }
    }
}
