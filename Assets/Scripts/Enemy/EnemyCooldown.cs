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
    private void Start()
    {
        effects = GetComponent<Effects>();
    }
    private void Update()
    {
        if (attacking)
        {
            return;
        }
        if (effects.cold)
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