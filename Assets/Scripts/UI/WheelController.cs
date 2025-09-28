using UnityEngine;
using UnityEngine.InputSystem;

public class WheelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private UIWheel _uiWheel;
    [SerializeField]
    private Entity _entity;

    [Header("Input Actions")]
    [SerializeField]
    private InputAction _goNextOptionAction;
    [SerializeField]
    private InputAction _goPrevOptionAction;
    [SerializeField]
    private InputAction _activeOptionAction;

    [Header("Data")]
    [SerializeField]
    private WheelData _wheelData;

    private int _currentSelectedIndex = 0;

    private bool _isWheelActive = false;

    private void Start()
    {
        if (_uiWheel == null)
        {
            Debug.LogError("WheelController: UIWheel reference is not set.");
            return;
        }
        _currentSelectedIndex = 0;
        _isWheelActive = false;
    }

    private void OnEnable()
    {
        _goNextOptionAction.Enable();
        _goPrevOptionAction.Enable();
        _activeOptionAction.Enable();

        _goNextOptionAction.performed += ctx => GoNextWheelOption();
        _goPrevOptionAction.performed += ctx => GoPreviousWheelOption();
        _activeOptionAction.performed += ctx => HandleActiveOption();

        UIWheelOption.OnMouseEnterOption += HandleOnMouseEnterOption;

        Entity.OnEntityClicked += HandleOnEntityClicked;

        AbilityExit.OnAbilityExitActivated += HandleAbilityExit;
    }

    private void OnDisable()
    {
        _goNextOptionAction.Disable();
        _goPrevOptionAction.Disable();
        _activeOptionAction.Disable();

        _goNextOptionAction.performed -= ctx => GoNextWheelOption();
        _goPrevOptionAction.performed -= ctx => GoPreviousWheelOption();
        _activeOptionAction.performed -= ctx => HandleActiveOption();

        UIWheelOption.OnMouseEnterOption -= HandleOnMouseEnterOption;

        Entity.OnEntityClicked -= HandleOnEntityClicked;

        AbilityExit.OnAbilityExitActivated -= HandleAbilityExit;
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
        _isWheelActive = false; // Needed? Maybe not
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

    private void HandleOnEntityClicked(Entity entity)
    {
        if(_isWheelActive)
        {
            return;
        }
        
        if (entity == _entity)
        {
            _isWheelActive = true;
           _uiWheel.ShowWheel(0, () => {
                _uiWheel.SetSelectionByIndex(_currentSelectedIndex, true);
            });
        }
    }

    private void HandleActiveOption()
    {
        if (!_isWheelActive) return;
        IActivable ability = _wheelData.GetAbilityByIndex(_currentSelectedIndex);
        if (ability != null)
        {
            ability.Activate(_entity);
        }
        else
        {
            Debug.LogError("WheelController: No ability found for the selected index.");
        }
    }

    private void HandleAbilityExit(Entity target)
    {
        _uiWheel.HideWheel(0,() => {
            ResetSelection();
        });
    }
}
