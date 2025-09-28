using UnityEngine;

[CreateAssetMenu(fileName = "AbilityVFX", menuName = "Abilities/AbilityVFX", order = 1)]
public class AbilityVFX : Ability
{
    [Header("VFX Settings")]
    [SerializeField]
    private GameObject _vfxPrefab;

    public override void Activate(Entity target)
    {
        if (_vfxPrefab != null)
        {
            Instantiate(_vfxPrefab, target.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("VFX Prefab is not assigned.");
        }
    }
}
