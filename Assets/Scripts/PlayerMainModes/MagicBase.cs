using UnityEngine;
public enum MagicType {
    Active,
    Passive
}
public abstract class MagicBase : ScriptableObject
{
    public float manaCost;
    public AudioClip audioClip;
    public GameObject magicParticles;
    public abstract void Cast();


    public MagicType magicType;
    public GameObject PassivePrefab;
    public string Name;
    public Sprite Image;
    public rarity Rarity;
    [TextArea]
    public string Description;
}