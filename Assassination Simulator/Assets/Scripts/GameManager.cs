using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;
    public GameState PrevGameState;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateGameState(State);
    }

    // Update is called once per frame
    public void UpdateGameState(GameState newState)
    {
        PrevGameState = State;
        State = newState;

        switch(newState) {
            case GameState.Pause:
                break;
            case GameState.Overworld:
                break;
            case GameState.MainMenu:
                break;
            case GameState.Minigame:
                break;
            case GameState.Death:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState {
        Pause,
        Overworld,
        MainMenu,
        Minigame,
        Transition,
        Death
    }