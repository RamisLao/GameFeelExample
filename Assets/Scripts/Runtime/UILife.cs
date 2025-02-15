using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image _heart;
    [SerializeField]
    private Color _colorFine;
    [SerializeField]
    private Color _colorDamaged;
    [SerializeField]
    private Color _colorAlert;
    [SerializeField]
    private TextMeshProUGUI _text;

    public void HealthChanged(int newHealth)
    {
        _text.text = newHealth.ToString();

        switch (newHealth)
        {
            case 0:
            case 1:
                _heart.color = _colorAlert;
                break;
            case 2:
                _heart.color = _colorDamaged;
                break;
            default:
                _heart.color = _colorFine;
                break;
        }
    }
}
