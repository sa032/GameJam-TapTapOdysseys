using System.Collections;
using UnityEngine;

public class DoAnimationEnable : MonoBehaviour
{
    public float DelayTime;
    void OnEnable()
    {
        StartCoroutine(DelayAnim());
    }
    IEnumerator DelayAnim()
    {
        yield return new WaitForSeconds(DelayTime);
        this.GetComponent<Animator>().Play(0);
    }
}
