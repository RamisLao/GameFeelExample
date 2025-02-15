using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;
    [SerializeField]
    private Color _damageColor;
    [SerializeField]
    private float _intervalDuration = 0.1f;

    public void Activate()
    {
        StartCoroutine(ActivateCoroutine());
    }

    private IEnumerator ActivateCoroutine()
    {
        Color currentColor = _renderer.color;

        _renderer.color = _damageColor;
        yield return new WaitForSeconds(_intervalDuration);
        _renderer.color = currentColor;
        yield return new WaitForSeconds(_intervalDuration);
        _renderer.color = _damageColor;
        yield return new WaitForSeconds(_intervalDuration);
        _renderer.color = currentColor;
    }
}
