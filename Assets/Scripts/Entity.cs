using UnityEngine;
using System;

public class Entity : MonoBehaviour
{
    public static event Action<Entity> OnEntityClicked;

    public void Click()
    {
        OnEntityClicked?.Invoke(this);
    }
}
