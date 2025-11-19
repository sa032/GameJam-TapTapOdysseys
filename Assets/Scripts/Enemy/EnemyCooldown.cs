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
    private void Update()
    {
        if (attacking)
        {
            return;
        }
        currentCoolDown -= Time.deltaTime;
        if (currentCoolDown < 0)
        {
            attackEvent.Invoke();
            currentCoolDown = coolDown;
        }
    }
}