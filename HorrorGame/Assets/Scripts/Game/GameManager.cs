using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Controller
public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager GetInstance() { return instance; }

    public enum GameState { Playing, Win, Fail, Inventory };
    public GameState state { private set; get; }

    public GameUI gameUI;

    public GameObject hero;


    void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        state = GameState.Playing;
    }


    // Update is called once per frame
    void Update()
    {
        CheckFailCondition();
    }


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


    public void Win()
    {
        state = GameState.Win;
        gameUI.ShowWin();
    }


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
}
