using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private int _startingHealth = 1;

    [Header("References")]
    [SerializeField]
    private GameObject _explosion;

    private int _health;

    public int StartingHealth => _startingHealth;

    public UnityEvent<int> OnDamage;
    public UnityEvent<int> OnHealing;
    public UnityEvent OnDeath;

    private void Start()
    {
        _health = _startingHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") ||
            collision.CompareTag("Missile"))
        {
            _health--;

            if (_health <= 0 )
            {
                OnDeath.Invoke();
                if (_explosion != null )
                    Instantiate(_explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            OnDamage.Invoke(_health);
        }
        else if (collision.TryGetComponent(out BoosterHealth booster)) 
        {
            _health += booster.HealthBoost;

            OnHealing.Invoke(_health);

            Destroy(booster.gameObject);
        }
    }
}
