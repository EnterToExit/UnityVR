using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("switched to game scene (from mainMenuController)");
    }
}
