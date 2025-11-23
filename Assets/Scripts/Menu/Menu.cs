using System.Collections;
using Map;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject[] canvaGameplay;
    public GameObject Mainmenu,Subtitle; 

    public GameObject Player;
    public Transform PlayerPositionCutscene;
    public Transform PlayerPositionGameplay;
    public ParticleSystem DimensionVFX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayingCutscene == true) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlayMainMenu());
        }
    }
    bool isPlayingCutscene = false;
    public IEnumerator PlayMainMenu()
    {
        TimeBarManager.instance.SwitchDataset(0);
        
        isPlayingCutscene = true;
        Player.transform.position = PlayerPositionCutscene.position;
        Mainmenu.GetComponent<Animator>().Play("Mainmenu_out");
        DimensionVFX.Play();
        yield return new WaitForSeconds(1f);
        //
        //
        
        //yield return new WaitForSeconds(0.125f);
        Subtitle.SetActive(true);
        LeanTween.move(Player,PlayerPositionGameplay,0.75f);
        yield return new WaitForSeconds(2.5f);
        Subtitle.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        
        foreach(GameObject g in canvaGameplay) g.SetActive(true);
        
        MapManager.Instance.CurrentFloor = 0;
        MapManager.Instance.GenerateNewMap();
        yield return new WaitForSeconds(0.25f);
        MapNodeSelectUI.instance.GetNextNodeUI();
        BuffContainData.instance.IsStartTimer = true;
        isPlayingCutscene = false;
        Mainmenu.SetActive(false);
    }

}
