using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    public Health playerHP;
    public Slider slider;
    public TextMeshProUGUI text;
    
    void Update()
    {
        if(playerHP == null) return;

        slider.value = playerHP.health/playerHP.maxHealth;
        text.text = "HP "+playerHP.health+"/"+playerHP.maxHealth;
    }
}
