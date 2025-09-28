using UnityEngine;
using UnityEngine.InputSystem;

public class MouseEntityClicker : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private InputAction _clickAction;

    private Camera _mainCamera;
  
    void Start()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _clickAction.Enable();

        _clickAction.performed += ctx => HandleMousePress();
    }

    private void OnDisable()
    {
        _clickAction.Disable();

        _clickAction.performed -= ctx => HandleMousePress();
    }

    private void HandleMousePress()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject hitObj = hit.collider.gameObject;
            Entity targetEntity = null;

            if(hitObj.TryGetComponent(out targetEntity))
            {
                if (EntityManager.Instance.AreAnyWheelActive() && !targetEntity.IsWheelActive())
                {
                    return;
                }
                targetEntity.Click();
            }
        }
    }
}
