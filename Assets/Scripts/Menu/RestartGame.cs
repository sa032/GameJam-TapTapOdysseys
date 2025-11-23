using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Map;

public class RestartGame : MonoBehaviour
{
    public GameObject[] WinLoseScene;
    public GameObject MainMenuCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimationINDone == true)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ClearAllObject();
                isAnimationINDone = false;
                RestartGameFunction();
            }
        }
    }
    bool isAnimationINDone = false;
    void OnEnable()
    {
        stats();
        ClearAllObject();
        isAnimationINDone = false;
        StartCoroutine(AnimationIN());
    }

    public void RestartGameFunction()
    {
        
        StartCoroutine(PlayMainMenu());
    }
    IEnumerator PlayMainMenu()
    {
        StartCoroutine(PlayCC());
        yield return null;
        
    }
    public IEnumerator AnimationIN()
    {
        yield return null;
        TimeBarManager.instance.SwitchDataset(3);
        foreach(GameObject n in WinLoseScene){
            Image[] images = n.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] Text_tmp = n.GetComponentsInChildren<TextMeshProUGUI>();
            yield return new WaitForSeconds(0.035f);
            for (int i = 0;i<=25;i++)
            {
                foreach(Image image in images)
                {
                    //if(image != this.GetComponent<Image>()){
                        Color temp = image.color;
                        image.color = new Color(temp.r,temp.g,temp.b,(float)i/25f);
                    //}
                }
                foreach(TextMeshProUGUI text in Text_tmp)
                {
                    Color temp = text.color;
                    text.color = new Color(temp.r,temp.g,temp.b,(float)i/25f);
                }
                yield return new WaitForSeconds(0.005f);
            }
            
        }
        isAnimationINDone = true;
    }
    public GameObject Mainmenu,Subtitle; 

    public GameObject Player;
    public Transform PlayerPositionCutscene;
    public Transform PlayerPositionGameplay;
    public ParticleSystem DimensionVFX;
    public IEnumerator PlayCC()
    {
        
        foreach(GameObject n in WinLoseScene){
            Image[] images = n.GetComponentsInChildren<Image>();
            TextMeshProUGUI[] Text_tmp = n.GetComponentsInChildren<TextMeshProUGUI>();
            yield return new WaitForSeconds(0.035f);
            for (int i = 0;i<=25;i++)
            {
                foreach(Image image in images)
                {
                    //if(image != this.GetComponent<Image>()){
                        Color temp = image.color;
                        image.color = new Color(temp.r,temp.g,temp.b,1-(float)i/25f);
                    //}
                }
                foreach(TextMeshProUGUI text in Text_tmp)
                {
                    Color temp = text.color;
                    text.color = new Color(temp.r,temp.g,temp.b,1-(float)i/25f);
                }
                yield return new WaitForSeconds(0.005f);
            }
            
        }
        TimeBarManager.instance.SwitchDataset(0);
        
        Player.transform.position = PlayerPositionCutscene.position;
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
        MapManager.Instance.CurrentFloor = 0;
        MapManager.Instance.GenerateNewMap();
        yield return new WaitForSeconds(0.25f);
        MapNodeSelectUI.instance.GetNextNodeUI();
        
        Mainmenu.SetActive(false);
        foreach(GameObject n in WinLoseScene) n.SetActive(false);
    }
    public TextMeshProUGUI Kill,Playtime,Itemcollect;
    void stats()
    {
        Kill.text = "Kill " + BuffContainData.instance.Kill;
        int totalSeconds = Mathf.FloorToInt(BuffContainData.instance.PlayTime); // แปลงเป็น int
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        Playtime.text = $"PlayTime {hours:00}:{minutes:00}:{seconds:00}";
        Itemcollect.text = "Item Collect "+Inventory.transform.childCount;
    }
    public GameObject Inventory,MagicManager,ObjectContainer,UIItem,BorderUI1,BorderUI2,NPC,title;
    void ClearAllObject()
    {
        LevelManager.instance.Dif_level = 0;
        LevelManager.instance.currentLevel = 1;
        LevelManager.instance.currentExp = 0;
        LevelManager.instance.expToNextLevel = 100;
        LevelManager.instance.predictedLevel = 1;
        LevelManager.instance.UpdateText();

        BorderUI1.SetActive(false);
        BorderUI2.SetActive(false);
        NPC.SetActive(false);
        title.SetActive(false);

        EventCard.Instance.isEnterEnemyNode = false;

        MapNodeSelectUI.instance.Card1.SetActive(false);
        MapNodeSelectUI.instance.Card2.SetActive(false);
        MapNodeSelectUI.instance.Card3.SetActive(false);

        MapNodeSelectUI.instance.NextUI.SetActive(false);

        BuffContainData.instance.DamageBuffFlat = 0;
        BuffContainData.instance.DamageBuffPercent = 0;

        BuffContainData.instance.DefenseBuffFlat = 0;
        BuffContainData.instance.DefenseBuffPercent = 0;

        BuffContainData.instance.HPBuffFlat = 0;
        BuffContainData.instance.HPBuffPercent = 0;

        BuffContainData.instance.IsStartTimer = false;
        BuffContainData.instance.Kill = 0;
        BuffContainData.instance.PlayTime = 0;
        for (int i = 0;i<Inventory.transform.childCount;i++)
        {
            Destroy(Inventory.transform.GetChild(i).gameObject);
        }
        for (int i = 0;i<MagicManager.transform.childCount;i++)
        {
            Destroy(MagicManager.transform.GetChild(i).gameObject);
        }
        for (int i = 0;i<ObjectContainer.transform.childCount;i++)
        {
            Destroy(ObjectContainer.transform.GetChild(i).gameObject);
        }
        for (int i = 0;i<UIItem.transform.childCount;i++)
        {
            Destroy(UIItem.transform.GetChild(i).gameObject);
        }
        Player.GetComponent<Health>().health = 20;
        Player.GetComponent<Health>().maxHealth = 20;
    }
}
