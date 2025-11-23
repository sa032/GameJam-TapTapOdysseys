using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Map;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [Header("UI Elements")]
    public Image fillImage;
    public TMP_Text levelText;
    public TMP_Text expText;

    [Header("EXP Settings")]
    public int currentLevel = 1;
    public float currentExp = 0f;
    public float expToNextLevel = 100f;

    [Header("Animation")]
    public float fillSpeed = 2f;
    public AnimationCurve fillCurve;

    // Queue สำหรับ EXP
    private Queue<float> expQueue = new Queue<float>();
    private bool isProcessing = false;

    [Header("VFX")]
    public ParticleSystem[] VFX;
    private bool particleQueued = false; // flag เล่น particle

    [Header("Flash Effect")]
    public Color flashColor = Color.white;
    public int flashCount = 2;
    public float flashSpeed = 5f;

    private Color originalColor;
    
    public int predictedLevel; // Level ที่คาดว่าจะถึงหลังเติม EXP ก้อนนี้
    public int Dif_level;


    private void Awake()
    {
        instance = this;
        originalColor = fillImage.color;
        UpdateUIInstant();
    }

    // เพิ่ม EXP → เข้าคิว
    public void AddExp(float amount)
    {
        expQueue.Enqueue(amount);
        predictedLevel = CalculatePredictedLevelFromQueue();
        MapNodeSelectUI.instance.GetDifLevel();
        if (!isProcessing)
            StartCoroutine(ProcessExpQueue());
    }

    IEnumerator ProcessExpQueue()
    {
        isProcessing = true;

        while (expQueue.Count > 0)
        {
            float amount = expQueue.Dequeue();
            yield return StartCoroutine(AnimateAddExp(amount));
        }

        isProcessing = false;
    }
    public int CalculatePredictedLevelFromQueue()
    {
        float tempExp = currentExp;
        float tempExpToNext = expToNextLevel;
        int tempLevel = currentLevel;

        // รวมทุก EXP ใน Queue
        foreach (float queuedExp in expQueue)
        {
            float expToAdd = queuedExp;

            while (expToAdd > 0)
            {
                float remainingExpToLevel = tempExpToNext - tempExp;
                float add = Mathf.Min(expToAdd, remainingExpToLevel);

                tempExp += add;
                expToAdd -= add;

                if (tempExp >= tempExpToNext)
                {
                    tempExp -= tempExpToNext;
                    tempLevel++;
                    tempExpToNext *= 1.05f;
                }
            }
        }
        
        return tempLevel;
    }


    IEnumerator AnimateAddExp(float amount)
{
    // คำนวณ predictedLevel ก่อนเพิ่มจริง
    

    // เพิ่ม EXP จริงทีละรอบ
    float expToAdd = amount;
    while (expToAdd > 0)
    {
        float remainingExpToLevel = expToNextLevel - currentExp;
        float add = Mathf.Min(expToAdd, remainingExpToLevel);

        currentExp += add;
        expToAdd -= add;

        // เติมแท่ง EXP
        float targetFill = currentExp / expToNextLevel;
        yield return AnimateFillTo(targetFill);

        // ถ้าเต็มเลเวล → เลเวลอัพ
        if (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            LevelUp();

            fillImage.fillAmount = currentExp / expToNextLevel;
            UpdateText();
        }
    }

    // เล่น Particle ถ้ามีเลเวลอัพเกิดขึ้น
    if (particleQueued)
    {
        foreach (ParticleSystem p in VFX)
            p.Play();
        particleQueued = false;
    }
}


    IEnumerator AnimateFillTo(float target)
    {
        float start = fillImage.fillAmount;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * fillSpeed;
            float curved = fillCurve.Evaluate(t);
            fillImage.fillAmount = Mathf.Lerp(start, target, curved);
            UpdateText();
            yield return null;
        }

        fillImage.fillAmount = target;
        UpdateText();
    }

    void LevelUp()
    {
        currentLevel++;
        expToNextLevel *= 1.05f;
        SoundManager.instance.PlaySoundSFX("LevelUp");

        // ตั้ง flag ให้เล่น particle หลัง AnimateAddExp เสร็จ
        particleQueued = true;

        // Flash effect ยังทำงานตามปกติ
        StartCoroutine(FlashFill());
    }

    IEnumerator FlashFill()
    {
        for (int i = 0; i < flashCount; i++)
        {
            // ไปสีกระพริบ
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * flashSpeed;
                fillImage.color = Color.Lerp(originalColor, flashColor, t);
                yield return null;
            }

            // กลับมาสีเดิม
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * flashSpeed;
                fillImage.color = Color.Lerp(flashColor, originalColor, t);
                yield return null;
            }
        }

        fillImage.color = originalColor;
    }

    public void UpdateText()
    {
        levelText.text = $"Level {currentLevel}";
        expText.text = $"{currentExp:0}/{expToNextLevel:0}";
    }

    void UpdateUIInstant()
    {
        fillImage.fillAmount = currentExp / expToNextLevel;
        UpdateText();
    }
}
