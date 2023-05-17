using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject healthPanel;
    public GameObject winPanel;
    public GameObject failPanel;
    public InventoryUIController inventoryPanel;

    public GameObject helloConversationPanel;
    private GameObject _currentPanel = null;

    public GameObject[] mobileOnlyViews;


    private void Start()
    {
        winPanel.SetActive(false);
        failPanel.SetActive(false);
        inventoryPanel.gameObject.SetActive(false);

        foreach (GameObject view in mobileOnlyViews)
        {
            //if (Application.isMobilePlatform)
            //{
            //    view.SetActive(true);
            //}
            //else
            //{
            //    view.SetActive(false);
            //}
        }

        
    }


    public void ShowWin()
    {
        winPanel.SetActive(true);
    }


    public void ShowFail()
    {
        failPanel.SetActive(true);
    }


    public void ShowInventory(bool doShow)
    {
        inventoryPanel.gameObject.SetActive(doShow);
    }


    public void ShowConversation(string conversationName)
    {
        if(conversationName == "Hello")
        {
            _currentPanel = helloConversationPanel;
            _currentPanel.SetActive(true);
        }
    }


    public void CloseCurrentConversation()
    {
        if(_currentPanel != null && _currentPanel.activeSelf == true)
        {
            _currentPanel.SetActive(false);
            _currentPanel = null;
        }
    }
}
