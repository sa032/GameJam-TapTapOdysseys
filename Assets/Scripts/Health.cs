using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public Animator anim;
    public string hurt;
    public string idle;
    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void damage(float damageAmount)
    {
        health -= damageAmount;
        StartCoroutine(Shake(5));
        Debug.Log(damageAmount);
    }
    public void heal(float healAmount)
    {
        health += healAmount;
        Debug.Log(healAmount);
    }
    private IEnumerator Shake(int times)
    {
        Vector3 OrigPos = transform.position;
        
        if (GlobalValues.blocking && this.gameObject.CompareTag("Player"))
        {
            Debug.Log("Blocked");
        }else
        if (anim!= null)
        {
            anim.Play(hurt);
        }

        for (int i = 0; i < times; i++)
        {
            transform.position = OrigPos + (Vector3)(Random.insideUnitCircle * 0.2f);
            yield return new WaitForSeconds(0.02f);
        }

        transform.position = OrigPos;
        yield return new WaitForSeconds(0.25f);

        if (GlobalValues.blocking && this.gameObject.CompareTag("Player"))
        {
            Debug.Log("Blocked");
        }else
        if (anim!= null)
        {
            anim.Play(idle);
        }
    }
}
