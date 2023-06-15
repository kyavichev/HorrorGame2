using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MessageBoxUIController : MonoBehaviour
{
    private static MessageBoxUIController instance = null;
    public static MessageBoxUIController GetInstance() { return instance; }


    public Text messageText;
    public Animator animator;

    public float defaultDuration = 2;
    private float timer = 0;


    private void Awake()
    {
        instance = this;
    }


    public void ShowMessage(string messageString, float duration)
    {
        messageText.text = messageString;
        animator.SetTrigger("Show");
        timer = duration;
    }


    public void ShowMessage(string messageString)
    {
        ShowMessage(messageString, defaultDuration);
    }


    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Max(0, timer);

            if(timer == 0)
            {
                animator.SetTrigger("Hide");
            }
        }
    }
}
