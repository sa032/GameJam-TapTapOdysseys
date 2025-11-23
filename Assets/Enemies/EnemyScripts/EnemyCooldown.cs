using UnityEngine;
using UnityEngine.Events;

public class EnemyCooldown : MonoBehaviour
{
    [Header("Cooldown")]
    public float coolDown;
    public float currentCoolDown;
    [Header("Events")]
    public UnityEvent attackEvent;
    [Header("Attacking")]
    public bool attacking;
    private float coolDownSpeed;
    private Effects effects;
    public Transform parent;
    private void Start()
    {
        effects = parent.GetComponent<Effects>();
    }
    private void Update()
    {
        if (attacking)
        {
            return;
        }
        if (effects.frozen)
        {
            coolDownSpeed = 0;
        }else if (effects.cold)
        {
            coolDownSpeed = 0.5f;
        }
        else
        {
            coolDownSpeed = 1;
        }
        currentCoolDown -= Time.deltaTime * coolDownSpeed;
        if (currentCoolDown < 0)
        {
            attackEvent.Invoke();
            currentCoolDown = coolDown;
        }
    }
}