using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject rootGO;
    public GameObject pauseMenuGO;
    public GameObject optionsMenuGO;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (GameManager.Instance.State == GameState.Paused) ResumeGame();
            //else PauseGame();
        }
    }

    private void PauseGame()
    {
        //GameManager.Instance.UpdateGameState(GameState.Paused);
        pauseMenuGO.SetActive(true);
        optionsMenuGO.SetActive(false);
        rootGO.SetActive(true);
    }

    public void ResumeGame()
    {
        rootGO.SetActive(false);
        //GameManager.Instance.UpdateGameState(GameState.Playing);
    }

    public void BackToMainMenu()
    {
        rootGO.SetActive(false);
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        //GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
}
