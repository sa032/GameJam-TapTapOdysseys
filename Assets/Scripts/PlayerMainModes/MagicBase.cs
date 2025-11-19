using UnityEngine;

public abstract class MagicBase : ScriptableObject
{
    public float manaCost;
    public abstract void Cast();
}