using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Missile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float _speed = 10f; // Speed of the missile
    [SerializeField]
    private bool _movesUp = true;
    [SerializeField]
    private GameObject _explosionPrefab;

    public UnityEvent OnMissileDestroyed;

    void Update()
    {
        // Move the missile forward in the direction it's facing
        MoveForward();
    }

    // Move the missile forward
    private void MoveForward()
    {
        Vector2 direction;
        if (_movesUp)
        {
            direction = Vector2.up;
        }
        else
        {
            direction = Vector2.down;
        }
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    // Handle 2D collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy the missile when it collides with something
        DestroyMissile();
    }

    // Destroy the missile
    private void DestroyMissile()
    {
        if (_explosionPrefab != null)
        {
            Instantiate(_explosionPrefab, 
                transform.position, Quaternion.identity);
        }
        OnMissileDestroyed.Invoke();
        // Add any additional logic here (e.g., spawn an explosion effect)
        Destroy(gameObject);
    }
}
