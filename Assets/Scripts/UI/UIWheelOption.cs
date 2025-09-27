
using UnityEngine;
using UnityEngine.UI;

public class UIWheelOption : MonoBehaviour
{
    public bool IsSelected { get; private set; }

    [Header("References")]
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private Image _backgroundImage;

    [Header("Settings")]
    [SerializeField]
    private Color _selectedColor = Color.yellow;
    private Color _deselectedColor = Color.white;

    private void Start()
    {
        HandleInitState();
    }

    public void SetSelection(bool isSelected)
    {
        if (isSelected == IsSelected)
            return;

        IsSelected = isSelected;

        if (IsSelected)
        {
            HandleSelectedState();
        }
        else
        {
            HandleDeselectedState();
        }
    }

    private void HandleInitState()
    {
        IsSelected = false;
        _iconImage.enabled = false; // TODO: The icon will be managed by the animation - or default true
        _backgroundImage.color = _deselectedColor;
    }

    private void HandleSelectedState()
    {
        _backgroundImage.color = _selectedColor;
    }

    private void HandleDeselectedState()
    {
        _backgroundImage.color = _deselectedColor;
    }
}
