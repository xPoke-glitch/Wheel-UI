using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityExit", menuName = "Abilities/AbilityExit", order = 1)]

public class AbilityExit : Ability
{
    public static event Action<Entity> OnAbilityExitActivated;

    public override void Activate(Entity target)
    {
        OnAbilityExitActivated?.Invoke(target);
    }
}
