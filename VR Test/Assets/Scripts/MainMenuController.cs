using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartButton()
    {
        // SceneManager.LoadScene(""); //TODO
        Debug.Log("switched to game scene (from mainMenuController)");
    }
}
