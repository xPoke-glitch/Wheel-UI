using UnityEngine;
using UnityEngine.InputSystem;

public class WheelController : MonoBehaviour
{
    public bool IsWheelActive { get; private set; }

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

    [Header("Settings")]
    [SerializeField]
    private float _wheelShowDelay = 1.0f;

    private int _currentSelectedIndex = 0;

    private void Start()
    {
        if (_uiWheel == null)
        {
            Debug.LogError("WheelController: UIWheel reference is not set.");
            return;
        }
        _currentSelectedIndex = 0;
        IsWheelActive = false;
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
        IsWheelActive = false; // Needed? Maybe not
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
        if(IsWheelActive)
        {
            return;
        }
        
        if (entity == _entity)
        {
            IsWheelActive = true;
           _uiWheel.ShowWheel(_wheelShowDelay, () => {
                _uiWheel.SetSelectionByIndex(_currentSelectedIndex, true);
            });
        }
    }

    private void HandleActiveOption()
    {
        if (!IsWheelActive) return;
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
