using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private Vector2 _spawnIntervalRange;
    [SerializeField]
    private GameObject[] _enemyPrefabs;

    [Header("References")]
    [SerializeField]
    private ScoreManager _scoreManager;

    public UnityEvent OnEnemyDeath;
    public UnityEvent OnMissileDeath;

    private List<Transform> _spawnPoints;
    private List<Enemy> _spawnedEnemies;

    private void Awake()
    {
        _spawnPoints = new();
        _spawnedEnemies = new();

        foreach (Transform t in transform)
        {
            _spawnPoints.Add(t);
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();

        foreach (Enemy e in _spawnedEnemies)
        {
            if (e != null)
                Destroy(e.gameObject);
        }

        _spawnedEnemies.Clear();
    }

    public void OnMissileDestroyed()
    {
        OnMissileDeath.Invoke();
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float waitInterval = Random.Range(_spawnIntervalRange.x,
                _spawnIntervalRange.y);
            yield return new WaitForSeconds(waitInterval);

            int enemyIdx = Random.Range(0, _enemyPrefabs.Length);
            int spawnIdx = Random.Range(0, _spawnPoints.Count);

            Transform spawnPoint = _spawnPoints[spawnIdx];

            GameObject go = _enemyPrefabs[enemyIdx];

            if (go.TryGetComponent(out Enemy enemyPrefab))
            {
                Enemy enemy = Instantiate(enemyPrefab,
                spawnPoint.position, enemyPrefab.transform.rotation);
                InitalizeEnemy(enemy);
            }
            else
            {
                GameObject enemyContainer = Instantiate(go,
                spawnPoint.position, go.transform.rotation);
                Enemy[] enemies = enemyContainer
                    .GetComponentsInChildren<Enemy>();
                if (enemies.Length > 0)
                {
                    foreach (Enemy e in enemies)
                    {
                        InitalizeEnemy(e);
                    }
                }
            }
          
        }
    }

    private void InitalizeEnemy(Enemy enemy)
    {
        enemy.OnDeath.AddListener(EnemyDied);
        if (enemy.TryGetComponent(out EnemyAttack enemyAttack))
        {
            enemyAttack.Spawner = this;
        }

        _spawnedEnemies.Add(enemy);
    }

    private void EnemyDied(Enemy enemy, int points)
    {
        _spawnedEnemies.Remove(enemy);
        _scoreManager?.AddPoints(points);
        OnEnemyDeath.Invoke();
    }
}
