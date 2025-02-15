using UnityEngine;

public class EnemyMovementStraight : Enemy
{
    [Header("Settings")]
    [SerializeField]
    private float _speed = 10f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime, Space.World);
    }
}
