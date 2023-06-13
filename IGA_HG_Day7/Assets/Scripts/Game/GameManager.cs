using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Game Manager - glue that helps different systems communicate
public class GameManager : MonoBehaviour
{
    // Reference to the only instance of the GameManager
    private static GameManager instance = null;
    // Static function to retrieve the only instance of the GameManager
    public static GameManager GetInstance() { return instance; }

    // Enum (type) that lists all of the possible game states
    public enum GameState { Playing, Win, Fail, Inventory, Conversation };
    // Variable that keeps track of the current game state
    public GameState state { private set; get; }

    // Reference to the camera follower
    public DampenedCameraFollower cameraFollower;
    // Reference to the Game UI
    public GameUI gameUI;

    // Reference to the Hero prefab
    public GameObject heroPrefab;
    // Reference to the current hero (could be null)
    public GameObject hero;

    // Starting position of the hero
    private Vector3 _heroStartPosition;

    
    // Awake is called whenever Game Manager is created by the Unity game engine
    void Awake()
    {
        // Store reference to the instance of this class in the static variable
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Store Hero's starting position
        _heroStartPosition = hero.transform.position;
        
        // Set current game state
        state = GameState.Playing;
    }


    // Update is called once per frame
    void Update()
    {
        CheckFailCondition();
    }


    // This function resets everything, so that the game can be played. Not everything is reset.
    public void ResetGame()
    {
        // Make sure that there is no current hero
        if (hero == null)
        {
            // Create a new hero instance from the prefab
            hero = Instantiate(heroPrefab);
            // Set position of the newly created hero
            hero.transform.position = _heroStartPosition;

            // Update camera follower to track the newly created hero
            cameraFollower.objectToTrack = hero.GetComponentInChildren<CameraPoint>().gameObject;
            
            // Update the UI
            gameUI.healthPanel.destructible = hero.GetComponent<Destructible>();
            gameUI.ResetPanels();
            
            // Change current state of the game back to Playing
            state = GameState.Playing;
        }
    }


    // This function reloads the scene, so everything is reset to its original state
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // This function checks if hero is still alive
    private void CheckFailCondition()
    {
        if(state == GameState.Fail)
        {
            return;
        }

        if(hero == null)
        {
            state = GameState.Fail;
            gameUI.ShowFail();
        }
    }


    // This function gets called if Win condition is met
    public void Win()
    {
        state = GameState.Win;
        gameUI.ShowWin();
    }


    // This function gets called whenever Inventory view is shown or hidden
    public void ToggleInventory()
    {
        if(state != GameState.Inventory)
        {
            state = GameState.Inventory;
            gameUI.ShowInventory(true);
        }
        else
        {
            state = GameState.Playing;
            gameUI.ShowInventory(false);
        }
    }


    // This function gets called whenever conversation needs to be shown
    // Takes conversation name as an argument
    public void ShowConversation(string conversationName)
    {
        gameUI.ShowConversation(conversationName);
    }


    public void HideCurrentConversation()
    {
        gameUI.CloseCurrentConversation();
    }
}
