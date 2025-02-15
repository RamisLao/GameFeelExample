using System.Collections;
using UnityEngine;

public class BoosterAttack : MonoBehaviour
{
    [SerializeField]
    private float _timeToLive = 5;

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
