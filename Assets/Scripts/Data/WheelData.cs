using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WheelData", menuName = "WheelData", order = 1)]
public class WheelData : ScriptableObject
{
    [Header("Wheel Abilities")]
    [SerializeField]
    private List<Ability> _abilities;

    public IActivable GetAbilityByIndex(int index)
    {
        if (index >= 0 && index < _abilities.Count)
        {
            return _abilities[index];
        }
        Debug.LogError("Ability index out of range.");
        return null;
    }
}
