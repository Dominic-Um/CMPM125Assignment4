using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleSceneManager : MonoBehaviour
{
    public static HandleSceneManager instance;
    private string currentScene;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        currentScene = sceneName;
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
