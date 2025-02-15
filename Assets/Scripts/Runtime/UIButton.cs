using DG.Tweening;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float _increment = 1.3f;
    [SerializeField]
    private float _speed = 0.5f;

    private Vector3 _startingScale;

    private void Awake()
    {
        _startingScale = transform.localScale;
    }

    public void ScaleUp()
    {
        transform.DOScale(_startingScale * _increment, _speed);
    }

    public void ScaleDown()
    {
        transform.DOScale(_startingScale, _speed);
    }
}
