using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
    public void MainMenuLoad()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("switched to main menu(gameOverMenuController)");
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("restart game from (gameOverMenuController)");
    }
}
