using UnityEngine;

public abstract class Ability : ScriptableObject, IActivable
{
    public abstract void Activate(Entity target);
}
