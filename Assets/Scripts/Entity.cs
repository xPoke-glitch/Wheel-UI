using UnityEngine;
using System;

public class Entity : MonoBehaviour
{
    public static event Action<Entity> OnEntityClicked;

    [Header("References")]
    [SerializeField]
    private WheelController _wheelController;

    public void Click()
    {
        if(IsWheelActive())
        {
            return;
        }
        OnEntityClicked?.Invoke(this);
    }

    public bool IsWheelActive()
    {
        return _wheelController.IsWheelActive;
    }
}
