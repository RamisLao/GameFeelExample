using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipControllerTranslate : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    private Vector2 _moveInput;
    private CharacterInput _playerInput;
    private InputAction _moveAction;
    private Camera mainCamera;

    private void Awake()
    {
        _playerInput = new CharacterInput();
        _moveAction = _playerInput.Player.Move;
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 moveVector = new Vector3(_moveInput.x, _moveInput.y, 0) * _moveSpeed * Time.deltaTime;
        transform.Translate(moveVector, Space.World);

        ClampPositionToScreenBounds();
    }

    private void ClampPositionToScreenBounds()
    {
        Vector3 pos = transform.position;
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(pos);

        viewportPos.x = Mathf.Clamp(viewportPos.x, 0f, 1f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0f, 1f);

        transform.position = mainCamera.ViewportToWorldPoint(viewportPos);
    }
}