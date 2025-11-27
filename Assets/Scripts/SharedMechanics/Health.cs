using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class Health : MonoBehaviour
{
    private Effects effects;
    public float maxHealth;
    public float health;
    public float Defense;
    public bool isBlock;
    public float LevelGain;
    public Animator anim;
    public string hurt;
    public string idle;
    public GameObject Gameover;
    private void Start()
    {
        effects = GetComponent<Effects>();
    }
    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health <= 0)
        {
            if (this.gameObject.tag == "Enemy")
            {
                BuffContainData.instance.Kill +=1;
                LevelManager.instance.AddExp(LevelGain);
                Destroy(gameObject);
            }
            if (this.gameObject.tag == "Player")
            {
                Gameover.SetActive(true);
            }
            
        }
    }
    public void damage(float damageAmount)
    {
        float dmg_reduction = 0;
        if(isBlock == true) dmg_reduction = 50;
        
        float dmgCal1 = damageAmount-Defense;

        if (effects.fragile)
        {
            dmgCal1 = dmgCal1*1.5f;
        }
        float damageCalculate = dmgCal1-(dmgCal1*(dmg_reduction/100));
        if(damageCalculate <= 0) damageCalculate = 0;
        
        health -= damageCalculate;
        StartCoroutine(Shake(5));
        Debug.Log(damageCalculate);
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
