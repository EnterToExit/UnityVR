using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _input;

    private void Start()
    {
        foreach (var action in _input)
        {
            Debug.Log(action);
        }
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