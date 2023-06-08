using UnityEngine;

// Main component that controls UI, organizes things between different UI components
public class GameUI : MonoBehaviour
{
    // Reference to the Health Panel Controller
    public HealthPanelUIController healthPanel;
    
    // Reference to the win panel
    public GameObject winPanel;
    // Reference to the fail panel that has ways to restart / reset experience
    public GameObject failPanel;
    // Reference to the Inventory panel, currently commented out
    public InventoryUIController inventoryPanel;

    // Reference to the Conversation Panel
    public GameObject helloConversationPanel;
    public GameObject anotherConversationPanel;
    // Reference to the current conversation panel (could be any panel)
    private GameObject _currentPanel = null;

    // Future feature: mobile only views    
    // public GameObject[] mobileOnlyViews;


    private void Start()
    {
        winPanel.SetActive(false);
        failPanel.SetActive(false);
        inventoryPanel.gameObject.SetActive(false);
        helloConversationPanel.SetActive(false);
        anotherConversationPanel.SetActive(false);

        // foreach (GameObject view in mobileOnlyViews)
        // {
        //     if (Application.isMobilePlatform)
        //     {
        //         view.SetActive(true);
        //     }
        //     else
        //     {
        //         view.SetActive(false);
        //     }
        // }
    }


    // This function resets the state of hte UI
    public void ResetPanels()
    {
        winPanel.SetActive(false);
        failPanel.SetActive(false);
        
        helloConversationPanel.SetActive(false);
    }


    // This functions shows win panel
    public void ShowWin()
    {
        winPanel.SetActive(true);
    }


    // This function show fail panel
    public void ShowFail()
    {
        failPanel.SetActive(true);
    }


    // This function shows or hides Inventory panel
    // Future feature
    public void ShowInventory(bool doShow)
    {
        inventoryPanel.gameObject.SetActive(doShow);
    }


    // This functions shows specific conversation panel based on the specified conversation name
    public void ShowConversation(string conversationName)
    {
        // If conversation name is hello, UI will show conversation panel
        if(conversationName == "Hello")
        {
            _currentPanel = helloConversationPanel;
            _currentPanel.SetActive(true);
        }
        else if (conversationName == "Another")
        {
            _currentPanel = anotherConversationPanel;
            _currentPanel.SetActive(true);
        }
    }


    // This functions closes whatever current conversation panel is shown
    public void CloseCurrentConversation()
    {
        if(_currentPanel != null && _currentPanel.activeSelf == true)
        {
            _currentPanel.SetActive(false);
            _currentPanel = null;
        }
    }


    // Function that is hooked up in the Editor that will reset the game
    public void ResetButtonPressed()
    {
        GameManager.GetInstance().ResetGame();
    }

    // Function that is hooked up in the Editor that will restart the game (reload the scene)
    public void RestartButtonPressed()
    {
        GameManager.GetInstance().RestartGame();
    }
}
