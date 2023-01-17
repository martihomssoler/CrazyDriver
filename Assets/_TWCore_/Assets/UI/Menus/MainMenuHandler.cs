using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        //GameManager.Instance.UpdateGameState(GameState.Playing);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
