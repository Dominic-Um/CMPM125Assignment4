using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event Action<GameState> OnGameStateChange;
    public GameState CurrentState { get; private set; }
    [SerializeField] private InputReader inputReader;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateGameState(GameState.Detective);
    }

    public void UpdateGameState(GameState newState)
    {
        CurrentState = newState;
        switch (newState)
        {
            case GameState.Detective:
                break;
            case GameState.Platforming:
                inputReader.SetGameplay();
                break;
        }
        OnGameStateChange?.Invoke(newState);
    }
}

public enum GameState
{
    Detective,
    Platforming
}
