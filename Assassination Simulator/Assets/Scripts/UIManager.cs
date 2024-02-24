using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool pausePhysics;
    [SerializeField] private List<Behaviour> componentsToPause;
    Dictionary<Behaviour, bool> prevBehaviorState;

    [SerializeField] private Rigidbody2D rb2d;
    private Vector2 rb2dPrevVelocity;
    private float rb2dPrevAngularVelocity;
    private bool rb2dPrevState;

    public static UIManager Instance;

    private void Start()
    {
        prevBehaviorState = new Dictionary<Behaviour, bool>();

        if(rb2d == null)
        {
            pausePhysics = false;
        }
    }
    
    private void Awake()
    {
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
            PauseGame(true);
            pauseMenu.SetActive(true);
        }
        else
        {
            PauseGame(false);
            pauseMenu.SetActive(false);
        }
    }

    public void PauseGame(bool isPaused)
    {
        // If the game is getting paused.
        if(isPaused)
        {
            // Keeps the current state of the stopped component and stops them.
            foreach(var component in componentsToPause)
            {
                if(component.enabled)
                {
                    prevBehaviorState.Add(component, component.enabled);
                    component.enabled = false;
                }
            }

            // Also stops the physics if checked.
            if(pausePhysics)
            {
                rb2dPrevVelocity = rb2d.velocity;
                rb2dPrevAngularVelocity = rb2d.angularVelocity;
                rb2d.Sleep();
            }
        }
        // If the game is already paused.
        else
        {
            // Starts the components again.
            foreach (var component in prevBehaviorState.Keys)
            {
                component.enabled = true;
            }

            prevBehaviorState.Clear();

            // Starts the physics too if they were stopped.
            if(pausePhysics)
            {
                rb2d.velocity = rb2dPrevVelocity;
                rb2d.angularVelocity = rb2dPrevAngularVelocity;
                rb2d.WakeUp();
            }
        }

    }

    public void ButtonPause()
    {
        GameManager.Instance.UpdateGameState(GameManager.Instance.PrevGameState); 
    }
}
