using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private InputActionReference _action;

    private void Start()
    {
        _action.action.performed += Test;
    }

    private void Test(InputAction.CallbackContext obj)
    {
        return;
    }

    public void Pause()
    {
        // pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        // _gameIsPaused = true;
        Debug.Log("Game is paused");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        // _gameIsPaused = false;
        Debug.Log("Game is resumed");
    }

    public void MenuLoad()
    {
        // SceneManager.LoadScene("NEED TO BE REPLACED BY MENU SCENE");
        Debug.Log("switched to main menu");
    }
}