using UnityEngine;


public class InputManager : MonoBehaviour
{
    public PlayerController playerController;


    // Update is called once per frame
    void Update()
    {
        if (playerController)
        {
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

            if(Input.GetKeyDown(KeyCode.I))
            {
                GameManager.GetInstance().ToggleInventory();
            }
        }
        else
        {
            Debug.LogError("PlayerController is not specified");
        }
    }
}
