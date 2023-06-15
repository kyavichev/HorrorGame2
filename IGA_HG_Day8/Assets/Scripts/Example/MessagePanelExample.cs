using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePanelExample : MonoBehaviour
{
    public Animator animator;

    public string message1 = "Hello All!!!";
    public string message2 = "Goobdye All!!!";
    private int _messageIndex = 1;


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    animator.SetTrigger("Show");
        //}
        if(Input.GetKeyDown(KeyCode.D))
        {
            if (_messageIndex == 1)
            {
                MessageBoxUIController.GetInstance().ShowMessage(message1);
                _messageIndex = 2;
            }
            else if (_messageIndex == 2)
            {
                MessageBoxUIController.GetInstance().ShowMessage(message2);
                _messageIndex = 1;
            }
        }
    }
}
