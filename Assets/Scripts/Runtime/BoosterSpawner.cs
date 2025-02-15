using System.Collections;
using UnityEngine;

public class BoosterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _boosters;
    [SerializeField]
    private Vector2 _rangeForInstantiating;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(_rangeForInstantiating[0],
                                          _rangeForInstantiating[1]);
            yield return new WaitForSeconds(waitTime);

            GameObject booster = _boosters[Random.Range(0, _boosters.Length)];
            float x = Random.Range(0f, 1f);
            float y = Random.Range(0f, 1f);
            Vector3 pos = new Vector3(x, y, 0f);
            pos = _mainCamera.ViewportToWorldPoint(pos);
            pos.z = 0f;
            Instantiate(booster, pos, Quaternion.identity);
        }
    }
}
