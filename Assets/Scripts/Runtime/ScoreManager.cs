using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private int _currentScore;

    public int CurrentScore => _currentScore;

    private void Awake()
    {
        _currentScore = 0;
    }

    public void AddPoints(int points)
    {
        _currentScore += points;
        _scoreText.text = _currentScore.ToString();
    }

    public void ResetScore()
    {
        _currentScore = 0;
        _scoreText.text = _currentScore.ToString();
    }
}
