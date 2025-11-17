using System.Collections.Generic;
using Map;
using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> items;
    public Transform Inventory;
    public static ItemManager instance;
    void Start()
    {
        instance = this;
        StartCoroutine(Setup());
    }
    IEnumerator Setup()
    {
        yield return null;
        ItemPoolConfig itemsConfig = MapManager.Instance.itemPoolConfig;
        foreach(GameObject g in itemsConfig.ItemPool)
        {
            items.Add(g);
        }
    }

    public List<GameObject> GetRandomItems(int count = 3)
    {
        // ถ้าของไม่ถึง ก็สุ่มเท่าที่มี
        int amount = Mathf.Min(count, items.Count);

        List<GameObject> result = new List<GameObject>(amount);
        List<GameObject> temp = new List<GameObject>(items); // copy list เพื่อสุ่มไม่ซ้ำ

        for (int i = 0; i < amount; i++)
        {
            int index = UnityEngine.Random.Range(0, temp.Count);
            result.Add(temp[index]);
            temp.RemoveAt(index); // ลบเพื่อกันซ้ำ
        }

        return result; // จะมี 1–3 ชิ้น ตามจำนวนของจริง
    }

    public void AddItemToInventory(GameObject item)
    {
        GameObject itemClone = Instantiate(item,Inventory);

        RemoveItemFromPool(item);
    }
    public void RemoveItemFromPool(GameObject item)
    {
        GameObject found = items.Find(x => x.name == item.name);

        if (found != null)
        {
            items.Remove(found);
        }
    }
}
