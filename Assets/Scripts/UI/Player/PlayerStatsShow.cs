using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStatsShow : MonoBehaviour
{
    public TextMeshProUGUI Damage,Defense,ExtraHP,Kill,PlayTime;
    public BuffContainData buff;
    [Header("Mana")]
    public Slider ManaSlider;
    public TextMeshProUGUI ManaText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Damage.text = "Damage "+((1+buff.DamageBuffFlat)*(1+buff.DamageBuffPercent/100)).ToString();
        Defense.text = "Defense "+buff.DefenseBuffFlat;
        ExtraHP.text = "ExtraHP "+buff.HPBuffFlat;

        // แสดงเวลา
        int totalSeconds = Mathf.FloorToInt(buff.PlayTime); // แปลงเป็น int
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        PlayTime.text = $"PlayTime {hours:00}:{minutes:00}:{seconds:00}";

        Kill.text = "Kill " + buff.Kill;
        ManaSlider.value = MagicManager.instance.currentMana/100;
        ManaText.text = "Mana "+MagicManager.instance.currentMana+"/"+100;
    }
}
