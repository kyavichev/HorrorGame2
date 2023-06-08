using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script triggers conversation once a hero enters the trigger zone
public class ConversationTrigger : MonoBehaviour
{
    public SimpleNPCController npcController;


    // Name of the conversation. UI should have one with this name
    public string conversationName = "Hello";


    // This function automatically gets called by Unity when a collider enters the trigger zone
    private void OnTriggerEnter(Collider otherCollider)
    {
        // Check if hero is the collider that entered the zone
        GameObject other = otherCollider.gameObject;
        if(other == GameManager.GetInstance().hero)
        {
            // Show conversation if it is
            GameManager.GetInstance().ShowConversation(conversationName);

            npcController.StartTalking();
        }
    }


    // This function automatically gets called by Unity when a collider exits the trigger zone
    private void OnTriggerExit(Collider otherCollider)
    {
        // Check if hero is the collider that entered the zone
        GameObject other = otherCollider.gameObject;
        if (other == GameManager.GetInstance().hero)
        {
            // Show conversation if it is
            GameManager.GetInstance().HideCurrentConversation();

            npcController.StopTalking();
        }
    }
}
