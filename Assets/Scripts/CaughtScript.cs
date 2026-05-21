using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CaughtScreen : MonoBehaviour
{
    public GameObject caughtPanel;

    public static CaughtScreen Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        if (caughtPanel != null)
            caughtPanel.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Retry();
        }
    }

    public void ShowCaught()
    {

        if (caughtPanel != null)
            caughtPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void Retry()
    {

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }
}