using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private Missile _missilePrefab;
    [SerializeField]
    private float _attackInterval;

    public EnemySpawner Spawner
    {
        set
        {
            _spawner = value;
        }
    }

    private EnemySpawner _spawner;

    private void Start()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Missile missile = Instantiate(_missilePrefab,
                transform.position, _missilePrefab.transform.rotation);
            if (_spawner != null)
            {
                missile.OnMissileDestroyed
                    .AddListener(_spawner.OnMissileDestroyed);
            }
            yield return new WaitForSeconds(_attackInterval);
        }
    }
}
