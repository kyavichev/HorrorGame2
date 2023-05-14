using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButtonController : MonoBehaviour
{
    public void OnButtonPressed()
    {
        string sceneName = LevelDataTracker.GetInstance().AdvanceLevelIndexAndGetScene();
        SceneManager.LoadScene(sceneName);
    }
}
