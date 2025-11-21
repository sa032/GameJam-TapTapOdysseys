using UnityEngine;

public abstract class MagicBase : ScriptableObject
{
    public float manaCost;
    public AudioClip audioClip;
    public abstract void Cast();
}