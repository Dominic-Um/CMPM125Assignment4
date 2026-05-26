using UnityEngine;

public class DetectiveCursor : MonoBehaviour
{
    [SerializeField] private Texture2D magnifyingGlassCursor;
    [SerializeField] private Vector2 hotspot = Vector2.zero;

    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Detective)
        {
            Cursor.SetCursor(magnifyingGlassCursor, hotspot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}