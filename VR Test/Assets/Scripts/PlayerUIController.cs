using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private InputActionReference _actionReference;
    [SerializeField] private GameObject _actionMenuUI;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private GameObject _chooseMenu;
    [SerializeField] private GameObject _chooseMenu2;
    private static bool _gameIsPaused;
    
    private void Awake()
    {
        _actionReference.action.performed += StartButton; //Exception
    }

    private void StartButton(InputAction.CallbackContext obj)
    {
        if (_gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void CloseChooseWindows()
    {
        _chooseMenu.SetActive(false);
        _chooseMenu2.SetActive(false);
    }

    private void Pause()
    {
        _gameIsPaused = true;
        _actionMenuUI.SetActive(false);
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Game is paused by controller");
    }

    private void Resume()
    {
        _gameIsPaused = false;
        _actionMenuUI.SetActive(true);
        _pauseMenuUI.SetActive(false);
        CloseChooseWindows();
        Time.timeScale = 1;
        Debug.Log("Game is resumed by controller");
    }
    
    public void ResumeButton()
    {
        _gameIsPaused = false;
        Time.timeScale = 1;
        Debug.Log("Game is resumed by keyboard");
    }

    public void MenuLoad()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("switched to main menu (playerUIController)");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("restarting level (playerUIController)");
    }
}