using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deathCause = default;
    [SerializeField] private Button retryButton= default;
    private Animator animator;
    private void Awake()
    {
        animator= GetComponent<Animator>();
    }
    public void Dead(string dead)
    {
        animator.SetTrigger("Died");
        GameManager.Instance.UpdateGameState(GameState.Death);
        deathCause.text= dead;
        retryButton.Select();
    }
    public void RetryButton() 
    {
        SceneManager.LoadScene("VascoScene");
        GameManager.Instance.UpdateGameState(GameState.Overworld);
    }
}
