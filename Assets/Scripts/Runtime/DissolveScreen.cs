using UnityEngine;

public class DissolveScreen : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public void Appear()
    {
        _animator.SetTrigger("Appear");
    }

    public void Disappear()
    {
        _animator.SetTrigger("Disappear");
    }
}
