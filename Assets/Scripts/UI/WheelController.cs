using UnityEngine;
using UnityEngine.InputSystem;

public class WheelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private UIWheel _uiWheel;

    [Header("Input Actions")]
    [SerializeField]
    private InputAction _goNextOptionAction;
    [SerializeField]
    private InputAction _goPrevOptionAction;


    private int _currentSelectedIndex = 0;


    private void Start()
    {
        if (_uiWheel == null)
        {
            Debug.LogError("WheelController: UIWheel reference is not set.");
            return;
        }
        _currentSelectedIndex = 0;

        // TEST ONLY
        _uiWheel.ShowWheel(2.0f, ()=> {
            _uiWheel.SetSelectionByIndex(_currentSelectedIndex, true);
        });
    }

    private void OnEnable()
    {
        _goNextOptionAction.Enable();
        _goPrevOptionAction.Enable();

        _goNextOptionAction.performed += ctx => GoNextWheelOption();
        _goPrevOptionAction.performed += ctx => GoPreviousWheelOption();

        UIWheelOption.OnMouseEnterOption += HandleOnMouseEnterOption;
    }

    private void OnDisable()
    {
        _goNextOptionAction.Disable();
        _goPrevOptionAction.Disable();

        _goNextOptionAction.performed -= ctx => GoNextWheelOption();
        _goPrevOptionAction.performed -= ctx => GoPreviousWheelOption();

        UIWheelOption.OnMouseEnterOption -= HandleOnMouseEnterOption;
    }


    private void GoNextWheelOption()
    {
        _uiWheel.SetSelectionByIndex(_currentSelectedIndex, false);
        _currentSelectedIndex = (_currentSelectedIndex + 1) % _uiWheel.GetOptionsCount();
        _uiWheel.SetSelectionByIndex(_currentSelectedIndex, true);
    }

    private void GoPreviousWheelOption()
    {
        _uiWheel.SetSelectionByIndex(_currentSelectedIndex, false);
        _currentSelectedIndex = (_currentSelectedIndex - 1 + _uiWheel.GetOptionsCount()) % _uiWheel.GetOptionsCount();
        _uiWheel.SetSelectionByIndex(_currentSelectedIndex, true);
    }

    private void ResetSelection()
    {
        _uiWheel.SetSelectionByIndex(_currentSelectedIndex, false);
        _currentSelectedIndex = 0;
    }

    private void HandleOnMouseEnterOption(UIWheelOption option)
    {
        int newIndex = _uiWheel.GetWheelOptionIndex(option);
        if (newIndex != -1 && newIndex != _currentSelectedIndex)
        {
            _uiWheel.SetSelectionByIndex(_currentSelectedIndex, false);
            _currentSelectedIndex = newIndex;
            _uiWheel.SetSelectionByIndex(_currentSelectedIndex, true);
        }
    }

    private void HandleOnMouseExitOption(UIWheelOption option)
    {
        // Optional: Implement logic for when the mouse exits an option, if needed.
    }
}
