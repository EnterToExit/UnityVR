using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _currentScore;

    private void Awake()
    {
        CharacterDeath.Dead += OnDead;
    }

    private void OnDestroy()
    {
        CharacterDeath.Dead -= OnDead;
    }

    private void OnDead(int points)
    {
        AddPoints(points);
    }

    public void AddPoints(int points)
    {
        _currentScore += points;
        _text.text = _currentScore.ToString();
    }
}