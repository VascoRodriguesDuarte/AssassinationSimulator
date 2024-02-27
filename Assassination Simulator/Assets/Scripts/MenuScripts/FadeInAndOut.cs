using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FadeInAndOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup image;
    [SerializeField] private float transitionSpeed;
    [SerializeField] private float transitionTimeSpeed;

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
            if(GameManager.Instance.PrevGameState == GameState.MainMenu && state != GameState.MainMenu)
            {
                FadeIn(1);
            }
            else if(GameManager.Instance.PrevGameState == GameState.Pause && state == GameState.MainMenu)
            {
                FadeIn(0);
            }
    }

    /// <summary>
    /// Private method called before the first frame.
    /// </summary>
    private void Start()
    {
        StartCoroutine(Transition());
    }

    public void GoBackMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    private void FadeIn(int value)
    {
        StartCoroutine(ReverseTransition(value));
    }

    /// <summary>
    /// Private Coroutine that does the fade out.
    /// </summary>
    private IEnumerator Transition()
    {
        PlayerInputGame.Instance.PlayerInputInstance.DeactivateInput();
        // While the image is visible, then it fades out.
        while(image.alpha != 0f)
        {
            image.alpha -= transitionSpeed;
            AudioListener.volume += transitionSpeed;

            if(image.alpha < 0f)
            {
                image.alpha = 0f;
            }
            if(AudioListener.volume > 1f)
            {
                AudioListener.volume = 1f;
            }

            yield return new WaitForSeconds(transitionTimeSpeed);
        }

        PlayerInputGame.Instance.PlayerInputInstance.ActivateInput();
    }

    /// <summary>
    /// Private Coroutine that does the fade out.
    /// </summary>
    private IEnumerator ReverseTransition(int value)
    {
        PlayerInputGame.Instance.PlayerInputInstance.DeactivateInput();
        // While the image is visible, then it fades out.
        while(image.alpha != 1f)
        {
            image.alpha += transitionSpeed;
            AudioListener.volume -= transitionSpeed;

            if(image.alpha > 1f)
            {
                image.alpha = 1f;
            }
            if(AudioListener.volume < 0f)
            {
                AudioListener.volume = 0f;
            }

            yield return new WaitForSeconds(transitionTimeSpeed);
        }

        SceneManager.LoadScene(value);
    }
}
