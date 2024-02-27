using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInputGame : MonoBehaviour
{
    public static event Action<InputValue> MoveInput;
    public static event Action<InputValue> SprintInput;
    public static PlayerInputGame Instance;
    public PlayerInput PlayerInputInstance;
    private GameState _prevGameStatePause;

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
        }

        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        if(state == GameState.Pause)
        {
            PlayerInputInstance.SwitchCurrentActionMap("Pause");
        }
        else if(state == GameState.Overworld)
        {
            PlayerInputInstance.SwitchCurrentActionMap("Default");
        }
        else if(state == GameState.Minigame)
        {
            PlayerInputInstance.SwitchCurrentActionMap("Minigame");
        }
        else if(state == GameState.MainMenu)
        {
            PlayerInputInstance.SwitchCurrentActionMap("MainMenu");
        }
        else if (state == GameState.Death)
        {
            PlayerInputInstance.SwitchCurrentActionMap("Death");
        }
    }

    private void OnMove(InputValue inputValue)
    {
        MoveInput?.Invoke(inputValue);
    }

    private void OnSprint(InputValue inputValue)
    {
        SprintInput?.Invoke(inputValue);
    }

    private void OnPause(InputValue inputValue)
    {
        if(GameManager.Instance.State == GameState.Overworld || GameManager.Instance.State == GameState.Minigame)
        {
            GameManager.Instance.UpdateGameState(GameState.Pause);
        }
        else if(GameManager.Instance.State == GameState.Pause)
        {
            GameManager.Instance.UpdateGameState(GameManager.Instance.PrevGameState);
        }
    }

    private void OnStart(InputValue inputValue)
    {
        GameManager.Instance.UpdateGameState(GameState.Overworld);
    }

}
