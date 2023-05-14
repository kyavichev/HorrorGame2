using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataTracker : MonoBehaviour
{
    private static LevelDataTracker instance = null;
    public static LevelDataTracker GetInstance() { return instance; }


    public LevelsData levelsData;
    public int levelIndex = 0;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public string AdvanceLevelIndexAndGetScene()
    {
        levelIndex++;
        levelIndex %= levelsData.levels.Length;

        LevelDataEntry levelDataEntry = levelsData.levels[levelIndex];
        return levelDataEntry.name;
    }
}
