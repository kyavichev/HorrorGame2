using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoaderButtonUIController : MonoBehaviour
{
    public string sceneName;
    
    public void OnButtonPressed()
    {
        SceneManager.LoadScene(sceneName);
    }
}
