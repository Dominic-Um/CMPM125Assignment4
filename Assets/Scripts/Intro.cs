using UnityEngine;
using UnityEngine.InputSystem;

public class IntroMessage : MonoBehaviour
{
    public GameObject introPanel;

    void Start()
    {
        introPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (!introPanel.activeSelf) return;

        if (Keyboard.current.anyKey.wasPressedThisFrame
            || Mouse.current.leftButton.wasPressedThisFrame)
        {
            DismissIntro();
        }
    }

    void DismissIntro()
    {
        introPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}