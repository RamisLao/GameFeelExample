using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField]
    private int _points = 1;
    [SerializeField]
    private int _startingHealth = 1;
    [Space]

    [Header("References")]
    [SerializeField]
    private GameObject _explosion;
    [Space]

    public UnityEvent<Enemy, int> OnDeath;
    public UnityEvent OnDamage;

    private int _health;

    private void Awake()
    {
        _health = _startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Missile"))
        {
            _health--;

            if (_health <= 0)
            {
                OnDeath.Invoke(this, _points);
                DestroyEnemy();
            }
            else
            {
                OnDamage.Invoke();
            }
        }

        if (collision.CompareTag("Player"))
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
