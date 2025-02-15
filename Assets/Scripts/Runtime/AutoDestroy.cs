using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float _timeToAutoDestroy = 5f;

    private void Start()
    {
        StartCoroutine(AutoDestroyCoroutine());
    }

    private IEnumerator AutoDestroyCoroutine()
    {
        yield return new WaitForSeconds(_timeToAutoDestroy);
        Destroy(gameObject);
    }
}
