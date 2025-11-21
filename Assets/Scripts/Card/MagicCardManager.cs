using System.Collections.Generic;
using Map;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagicCardManager : MonoBehaviour
{
    public List<MagicBase> Magics;
    public Transform MagicPassiveInventory;
    public GameObject BorderMagicUI;
    public GameObject CardPreview,CardPreviewPosiion,SwitchUI;
    public ParticleSystem GetSkillVFX,SwicthVFX;
    public static MagicCardManager instance;
    public int CardAmount;
    void Start()
    {
        MagicPassiveInventory = this.transform;
        instance = this;
        /*MapNodeSelectUI.instance.Card1.GetComponent<CardContainData>().CardEvent += BorderDB;
        MapNodeSelectUI.instance.Card2.GetComponent<CardContainData>().CardEvent += BorderDB;
        MapNodeSelectUI.instance.Card3.GetComponent<CardContainData>().CardEvent += BorderDB;*/
        StartCoroutine(Setup());
    }
    IEnumerator Setup()
    {
        yield return null;
        
    }

    public List<MagicBase> GetRandomMagic(int count = 3)
    {
        // ถ้าของไม่ถึง ก็สุ่มเท่าที่มี
        int amount = Mathf.Min(count, Magics.Count);
        SoundManager.instance.PlaySoundSFX("MagicCardPick");
        List<MagicBase> result = new List<MagicBase>(amount);
        List<MagicBase> temp = new List<MagicBase>(Magics); // copy list เพื่อสุ่มไม่ซ้ำ

        for (int i = 0; i < amount; i++)
        {
            int index = UnityEngine.Random.Range(0, temp.Count);
            result.Add(temp[index]);
        }

        return result; // จะมี 1–3 ชิ้น ตามจำนวนของจริง
    }

    public void AddMagicToInventory(MagicBase magic)
    {
        /*ItemDataContain itemData = item.GetComponent<ItemDataContain>();
        GameObject itemClone = Instantiate(item,Inventory);
        GameObject ItemUI_Clone = Instantiate(ItemUI,InventoryUI);
        ItemUI_Clone.transform.GetChild(0).GetComponent<Image>().sprite = itemData.image;
        StartCoroutine(PositionUI(ItemUI_Clone));
        RemoveItemFromPool(item);*/
        switch (magic.magicType)
        {
            case MagicType.Active :
                MagicSkillAdd(magic);
                break;
            case MagicType.Passive :
                GetSkillVFX.Play();
                SoundManager.instance.PlaySoundSFX("MagicGet");
                Instantiate(magic.PassivePrefab,MagicPassiveInventory);
                StartCoroutine(NextNodeUIFunction());
                break;
        }
    }

    void MagicSkillAdd(MagicBase magicbase)
    {
        MagicBase[] magicslot = MagicManager.instance.magicSlots;
        bool Is_SlotEmpty = false;
        int i = 0;
        foreach (MagicBase magic in magicslot)
        {
            if(magic == null){
                Is_SlotEmpty = true;
                magicslot[i] = magicbase;
                GetSkillVFX.Play();
                StartCoroutine(NextNodeUIFunction());
                SoundManager.instance.PlaySoundSFX("MagicGet");
                break;
            }
            i++;
        }
        if (Is_SlotEmpty == false)
        {
            StartCoroutine(SwitchMagicCard());
        }
        
    }
    IEnumerator SwitchMagicCard()
    {
        SoundManager.instance.PlaySoundSFX("MagicSwitch");
        MapNodeSelectUI.instance.Title.SetActive(false);
        CardContainData.instance.StopAllCoroutines();
        GameObject[] AllCards = GameObject.FindGameObjectsWithTag("Card");
        foreach(GameObject card in AllCards) card.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        SwitchUI.SetActive(true);
        CardPreview.SetActive(true);
        ShowCardUI_inSlot(3);
    }
    IEnumerator NextNodeUIFunction()
    {
        CardContainData.instance.StopAllCoroutines();
        GameObject[] AllCards = GameObject.FindGameObjectsWithTag("Card");
        foreach(GameObject card in AllCards){card.SetActive(false);};
        yield return new WaitForSeconds(0.1f);
        MapNodeSelectUI.instance.Title.SetActive(false);
        if(LevelManager.instance.Dif_level != 0)
        {
            LevelManager.instance.Dif_level -=1;
        }
        MapNodeSelectUI.instance.GetNextNodeUI();
    }
    public void CardPreviewFunc(MagicBase magicBase, Vector3 pos)
    {
        CardContainData CardData = CardPreview.GetComponent<CardContainData>();
        CardData.state = CardState.Magic;
        CardData.MagicData = magicBase;
        
        
        CardPreview.transform.localPosition = pos;
        LeanTween.move(CardPreview,CardPreviewPosiion.transform.position,0.35f);
    }
    public void ShowCardUI_inSlot(int CardAmount)
    {
        
        BorderMagicUI.GetComponent<Image>().color = new Color(1.000f, 0.157f, 0.157f, 1.000f);
        BorderMagicUI.SetActive(false);
        GameObject Card1 = MapNodeSelectUI.instance.Card1;
        GameObject Card2 = MapNodeSelectUI.instance.Card2;
        GameObject Card3 = MapNodeSelectUI.instance.Card3;

        List<GameObject> Cards = new List<GameObject>();
        if(CardAmount > 0) Cards.Add(Card1);
        if(CardAmount >= 2) Cards.Add(Card2);
        if(CardAmount >= 3) Cards.Add(Card3);
            
        int i = 0;
        BorderMagicUI.SetActive(true);
        foreach(GameObject card in Cards)
        {
            card.SetActive(false);
            card.GetComponent<CardContainData>().state = CardState.MagicSwitch;
            card.GetComponent<CardContainData>().MagicData = MagicManager.instance.magicSlots[i];
            card.SetActive(true);
            i++;
        }
    }
    public void SwicthMagic(int index)
    {
        MagicManager.instance.magicSlots[index] = CardPreview.GetComponent<CardContainData>().MagicData;
        SwicthVFX.Play();
        SwitchUI.SetActive(false);
        CardPreview.SetActive(false);
        BorderDB();
        SoundManager.instance.PlaySoundSFX("Swicth");
        StartCoroutine(NextNodeUIFunction());
    }
    public void BorderShow(Color color)
    {
        BorderMagicUI.GetComponent<Image>().color = color;
        BorderMagicUI.SetActive(true);

    }
    public void BorderDB()
    {
        BorderMagicUI.SetActive(false);
    }
    
}
