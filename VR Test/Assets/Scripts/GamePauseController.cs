using UnityEngine;

public class GamePauseController : MonoBehaviour
{
    private static bool _gameIsPaused;

    private void Update()
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

    private void Pause()
    {
        // pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        _gameIsPaused = true;
    }

    private void Resume()
    {
        Time.timeScale = 1;
        _gameIsPaused = false;
    }
}