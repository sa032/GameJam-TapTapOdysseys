using UnityEngine;

public enum rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

public class ItemDataContain : MonoBehaviour
{
    public Sprite image;
    public string Name;
    public rarity Rarity;
    [TextArea]
    public string description;
}
