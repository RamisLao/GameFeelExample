using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float _timeForAppear = 0.75f;
    [SerializeField]
    private float _timeBeforeStartSpawningEnemies = 3f;

    [Header("References")]
    [SerializeField]
    private GameObject _startScreenPanel;
    [SerializeField]
    private GameObject _endScreenPanel;
    [SerializeField]
    private GameObject _hudPanel;
    [SerializeField]
    private GameObject _spaceship;
    [SerializeField]
    private Transform _spaceshipStartingPos;
    [SerializeField]
    private EnemySpawner _enemySpawner;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private MusicManager _musicManager;
    [SerializeField]
    private DissolveScreen _dissolveScreen;
    [SerializeField]
    private TextMeshProUGUI _textPoints;
    [SerializeField]
    private ButtonsVertical _startScreenButtons;
    [SerializeField]
    private ButtonsVertical _endScreenButtons;
    [SerializeField]
    private UILife _uiLife;
    [SerializeField]
    private BoosterSpawner _boosterManager;

    public UnityEvent OnStartGame;
    public UnityEvent OnPlayerDeath;

    private void Start()
    {
        _musicManager?.StartMainMusic();
        GoBackToStartScreen();
    }

    public void StartNewGame()
    {
        StartCoroutine(StartNewGameCoroutine());
    }

    public void GoBackToStartScreen()
    {
        Cursor.visible = true;
        _hudPanel.SetActive(false);
        _endScreenPanel.SetActive(false);
        _startScreenPanel.SetActive(true);
        _startScreenButtons.EnableButtons();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void GoToEndScreen()
    {
        Cursor.visible = true;
        StartCoroutine(GoToEndScreenCoroutine());
    }

    private IEnumerator StartNewGameCoroutine()
    {
        _dissolveScreen?.Appear();
        OnStartGame.Invoke();
        Cursor.visible = false;

        yield return new WaitForSeconds(_timeForAppear);

        GameObject spaceship = Instantiate(_spaceship, 
            _spaceshipStartingPos.position, _spaceship.transform.rotation);
        PlayerHealth health = spaceship.GetComponent<PlayerHealth>();
        health.OnDeath.AddListener(OnPlayerHasDied);
        if (_uiLife != null)
        {
            health.OnDamage.AddListener(_uiLife.HealthChanged);
            health.OnHealing.AddListener(_uiLife.HealthChanged);
            _uiLife.HealthChanged(health.StartingHealth);
        }
        _scoreManager?.ResetScore();

        _hudPanel.SetActive(true);

        _startScreenPanel.SetActive(false);
        _endScreenPanel.SetActive(false);

        _dissolveScreen?.Disappear();
        _musicManager?.StartInGameMusic();

        yield return new WaitForSeconds(_timeBeforeStartSpawningEnemies);

        _enemySpawner.StartSpawning();
        _boosterManager?.StartSpawning();
    }

    private IEnumerator GoToEndScreenCoroutine()
    {
        _enemySpawner.StopSpawning();
        _boosterManager?.StopSpawning();

        yield return new WaitForSeconds(_timeForAppear * 2);

        _endScreenPanel.SetActive(true);
        _endScreenButtons.EnableButtons();
        _musicManager?.StartMainMusic();
    }

    private void OnPlayerHasDied()
    {
        OnPlayerDeath.Invoke();
        _musicManager?.StopMusic();
        _hudPanel.SetActive(false);

        if (_textPoints != null)
        {
            string finalPoints;
            if (_scoreManager != null &&
                _scoreManager.CurrentScore != 1)
            {
                finalPoints = $"{_scoreManager.CurrentScore} puntos";
            }
            else
            {
                finalPoints = "1 punto";
            }
            _textPoints.text = finalPoints;
        }

        GoToEndScreen();
    }
}
