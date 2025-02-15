using Mono.Cecil.Cil;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SpaceshipControllerPhysics : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private bool _usePhysics = false;
    [SerializeField]
    private float _translateSpeed = 5f;
    [SerializeField]
    public float _thrustForce = 10f;
    [SerializeField]
    public float drag = 2f;

    [Header("Attack")]
    [SerializeField]
    private GameObject _missileForwardPrefab;
    [SerializeField]
    private float _missileSimpleCooldown = 0.3f;
    [SerializeField]
    private float _missileBoostedCooldown = 0.15f;
    [SerializeField]
    private Transform _singleAttackOrigin;
    [SerializeField]
    private Transform _doubleAttackOrigin1;
    [SerializeField]
    private Transform _doubleAttackOrigin2;
    [SerializeField]
    private float _attackBoosterDuration = 10;

    public UnityEvent OnBoosted;
    public UnityEvent OnSimpleAttack;
    public UnityEvent OnBoostedAttack;

    private Vector2 _moveInput;
    private CharacterInput _playerInput;
    private Camera _mainCamera;
    private Rigidbody2D _rb;

    private float _attackCurrentCooldown;
    private float _remainingAttackBoosterDuration;
    private bool _attackIsBoosted = false;

    private void Awake()
    {
        _playerInput = new CharacterInput();
        _playerInput.Player.Move.performed += OnMove;
        _playerInput.Player.Move.canceled += OnMove;
        _playerInput.Player.Attack.performed += OnAttack;
        _playerInput.Player.Enable();
        _attackIsBoosted = false;

        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
        }
        _rb.gravityScale = 0;
        _rb.linearDamping = drag;

        _attackCurrentCooldown = 0;
    }

    private void OnDisable()
    {
        _playerInput.Player.Disable();
    }

    private void Update()
    {
        if (!_usePhysics)
        {
            ApplyTranslateMovement();
        }
    }

    private void FixedUpdate()
    {
        if (_usePhysics)
            ApplyPhysicsMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BoosterAttack booster))
        {
            StartCoroutine(AttackBoosterCoroutine());
            Destroy(booster.gameObject);
        }
    }

    private IEnumerator AttackBoosterCoroutine()
    {
        _attackIsBoosted = true;
        OnBoosted.Invoke();
        _remainingAttackBoosterDuration = _attackBoosterDuration;

        while (_remainingAttackBoosterDuration > 0)
        {
            _remainingAttackBoosterDuration -= Time.deltaTime;
            yield return null;
        }

        _attackIsBoosted = false;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void ApplyTranslateMovement()
    {
        Vector3 moveVector = new Vector3(_moveInput.x, _moveInput.y, 0) * _translateSpeed * Time.deltaTime;
        transform.Translate(moveVector, Space.World);

        ClampPositionToScreenBounds();
    }

    private void ApplyPhysicsMovement()
    {
        _rb.AddForce(_moveInput * _thrustForce);

        ClampPositionToScreenBounds();
    }

    private void ClampPositionToScreenBounds()
    {
        Vector3 pos = transform.position;
        Vector3 viewportPos = _mainCamera.WorldToViewportPoint(pos);

        viewportPos.x = Mathf.Clamp(viewportPos.x, 0f, 1f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0f, 1f);

        transform.position = _mainCamera.ViewportToWorldPoint(viewportPos);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (_attackCurrentCooldown > 0) return;

        InstantiateAttack();
        StartCoroutine(AttackCooldownCoroutine());
    }

    private void InstantiateAttack()
    {
        if (!_attackIsBoosted)
        {
            Instantiate(_missileForwardPrefab, _singleAttackOrigin.position, 
                _missileForwardPrefab.transform.rotation);
            OnSimpleAttack.Invoke();
        }
        else
        {
            Instantiate(_missileForwardPrefab, _doubleAttackOrigin1.position,
                _missileForwardPrefab.transform.rotation);
            Instantiate(_missileForwardPrefab, _doubleAttackOrigin2.position,
                _missileForwardPrefab.transform.rotation);
            OnBoostedAttack.Invoke();
        }
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        _attackCurrentCooldown = _attackIsBoosted ?
            _missileBoostedCooldown :
            _missileSimpleCooldown;

        while (_attackCurrentCooldown > 0)
        {
            _attackCurrentCooldown -= Time.deltaTime;
            yield return null;
        }
    }
}
