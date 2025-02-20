using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float _shakeDuration = 0.5f;  // Duration of the shake effect
    [SerializeField]
    private float _shakeMagnitude = 0.2f; // Intensity of the shake
    [SerializeField]
    private float _dampingSpeed = 2.0f;   // How fast the shake ends

    private Vector3 _originalPosition;
    private float _currentShakeDuration = 0f;

    void Start()
    {
        _originalPosition = transform.localPosition;
    }

    public void StartShake()
    {
        StartCoroutine(StartShakeCoroutine());
    }

    private IEnumerator StartShakeCoroutine()
    {
        _currentShakeDuration = _shakeDuration;

        while (_currentShakeDuration > 0)
        {
            transform.localPosition = _originalPosition + Random.insideUnitSphere * _shakeMagnitude;
            _currentShakeDuration -= Time.deltaTime * _dampingSpeed;
            yield return null;
        }

        _currentShakeDuration = 0f;
        transform.localPosition = _originalPosition;
    }
}

