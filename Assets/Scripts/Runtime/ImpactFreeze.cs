using System.Collections;
using UnityEngine;

public class ImpactFreeze : MonoBehaviour
{
    [SerializeField]
    private float _freezeDuration = 0.5f;

    public void StartFreeze()
    {
        StartCoroutine(StartFreezeCoroutine());
    }

    private IEnumerator StartFreezeCoroutine()
    {
        Debug.Log(Time.timeScale);
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(_freezeDuration);

        Time.timeScale = 1;
    }
}
