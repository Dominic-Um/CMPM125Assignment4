using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class WinScreen : MonoBehaviour
{
    public GameObject winPanel;
    public static WinScreen Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
            Retry();
    }

    public void ShowWin()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}