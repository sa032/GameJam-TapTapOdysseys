using UnityEngine;
using UnityEngine.Events;

public enum EncounterType
{
    None,
    Exp
}
[CreateAssetMenu]
public class Encounter : ScriptableObject
{
    public string dialouge;
    public Sprite imageNPC;
    public EncounterType type;
    public float amount;
}
