using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    public void MainMenuLoad()
    {
        // SceneManager.LoadScene("MainMenu"); //TODO
        Debug.Log("switched to main menu(gameOverMenuController)");
    }
    
    public void RestartGame()
    {
        // SceneManager.LoadScene(""); //TODO
        Debug.Log("restart game from (gameOverMenuController)");
    }
}
