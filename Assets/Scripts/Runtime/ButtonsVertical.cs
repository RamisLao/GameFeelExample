using UnityEngine;
using UnityEngine.UI;

public class ButtonsVertical : MonoBehaviour
{
    private Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();
    }

    public void EnableButtons()
    {
        foreach (Button button in _buttons)
        {
            button.enabled = true;
        }
    }

    public void DisableButtons()
    {
        foreach (Button button in _buttons)
        {
            button.enabled = false;
        }
    }
}
