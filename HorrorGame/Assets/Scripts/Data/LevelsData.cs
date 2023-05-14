using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelDataEntry
{
    public string name;
    public int difficulty;
}


[CreateAssetMenu(fileName = "LevelsData", menuName = "Horror Game / Create new Levels Data")]
public class LevelsData : ScriptableObject
{
    public LevelDataEntry[] levels;
}
