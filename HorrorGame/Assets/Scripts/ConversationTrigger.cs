using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    public string conversationName = "Hello";


    private void OnTriggerEnter(Collider otherCollider)
    {
        GameObject other = otherCollider.gameObject;
        if(other == GameManager.GetInstance().hero)
        {
            GameManager.GetInstance().ShowConversation(conversationName);
        }
    }
}
