using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuController : MonoBehaviour
{
    public void MainMenuLoad()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("switched to main menu(winMenuController)");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("restart game from (winMenuController)");
    }
}