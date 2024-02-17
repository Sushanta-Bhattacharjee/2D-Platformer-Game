using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public Button buttonRestart;
    public Button buttonQuit;
    private void Awake()
    {
        buttonRestart.onClick.AddListener(ReloadLevel);
        // buttonQuit.onClick.AddListener(PlayGame);
    }

    public void PlayerDied()
    {
        gameObject.SetActive(true);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }

    // private void PlayGame()
    // {
    //     SceneManager.LoadScene(0);
    // }
}
