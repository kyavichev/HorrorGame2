using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartButtonController : MonoBehaviour
{
    public void OnButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
