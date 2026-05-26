using UnityEngine;

public class CallLoadScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        HandleSceneManager.instance.LoadScene(sceneName);
    }
}
