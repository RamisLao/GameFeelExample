using UnityEngine;

public class EnemyMovementSin : Enemy
{
    [Header("Settings")]
    [SerializeField]
    private float _speed = 3f;      // Downward movement speed
    [SerializeField]
    private float _waveSpeed = 2f;  // Speed of the side-to-side wave motion
    [SerializeField]
    private float _waveSize = 2f;   // How wide the wave is

    private float _startX;

    private void Start()
    {
        _startX = transform.position.x;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = _startX + Mathf.Sin(Time.time * _waveSpeed) * _waveSize;
        float y = transform.position.y - _speed * Time.deltaTime;

        transform.position = new Vector2(x, y);
    }
}
