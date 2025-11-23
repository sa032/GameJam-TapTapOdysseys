using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    void Start()
    {
        instance = this;
    }
    public void PlaySoundSFX(string source)
    {
        this.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sound/"+source));
    }
    public void PlayMusic(AudioClip audioClip)
    {
        this.transform.GetChild(0).GetComponent<AudioSource>().clip = audioClip;
        this.transform.GetChild(0).GetComponent<AudioSource>().Play();
    }
}
