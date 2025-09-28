using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    [Header("References")]
    [SerializeField]
    private List<Entity> _entities;

    public bool AreAnyWheelActive()
    {
        foreach (var entity in _entities)
        {
            if (entity.IsWheelActive())
            {
                return true;
            }
        }
        return false;
    }
}
