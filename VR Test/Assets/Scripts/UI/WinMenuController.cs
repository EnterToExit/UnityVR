using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private float _requiredScore;

    private void Update()
    {
        var currentScore = Convert.ToInt32(_text.text);
        if (currentScore < _requiredScore) return;

        Time.timeScale = 0;
        _winMenu.SetActive(true);
    }

    public void MainMenuLoad()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("switched to main menu(winMenuController)");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
        Debug.Log("restart game from (winMenuController)");
    }
}