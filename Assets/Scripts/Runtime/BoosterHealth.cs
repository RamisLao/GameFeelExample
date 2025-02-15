using System.Collections;
using UnityEngine;

public class BoosterHealth : MonoBehaviour
{
    [SerializeField]
    private int _healthBoost = 2;
    [SerializeField]
    private float _timeToLive = 5;

    public int HealthBoost => _healthBoost;

    private void Start()
    {
        StartCoroutine(AutoDestroyCoroutine());
    }

    private IEnumerator AutoDestroyCoroutine()
    {
        yield return new WaitForSeconds(_timeToLive);
        Destroy(gameObject);
    }
}
