using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerLevelUp : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private int initialExp;
    [SerializeField] private int expIncreasePerLevel;
    [SerializeField] private float constantA;
    [SerializeField] private float hideTime;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject fill;
    [SerializeField] private ParticleSystem levelUpEffect;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<Vector2> minAnchor;
    [SerializeField] private List<Vector2> maxAnchor;
    [SerializeField] private Transform bodyUI;
    [SerializeField] private float delay;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private Player player;
    [SerializeField] private UIDisplay displayUI;
    private const int maxPowerPiker = 3;
    private int currentLevel;
    private int currentExp;
    private void Start()
    {
        currentExp = 0;
        currentLevel = 1;
        expSlider.maxValue = initialExp;
        expSlider.value = currentExp;
    }
    public int GetLevel()
    {
        return currentLevel;
    }
    public void CollectExp(int exp)
    {
        currentExp += exp;
        expSlider.value = currentExp;
        StartCoroutine(HideBar());
        if(currentExp >= initialExp) { 
            LevelUp();
        }
    }
    private IEnumerator HideBar()
    {
        background.SetActive(true);
        fill.SetActive(true);
        yield return new WaitForSeconds(hideTime);
        background.SetActive(false);
        fill.SetActive(false);
    }
    private void LevelUp()
    {
        currentLevel++;
        LevelUpEffectPlay();   
        ResetExpBar();
        StartCoroutine(WaitToInvokePowerPicker());
    }
    private void LevelUpEffectPlay()
    {
        if(levelUpEffect != null)
        {
            ParticleSystem instance = Instantiate(levelUpEffect, playerTransform.position, Quaternion.identity);
            Destroy(instance, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    private void ResetExpBar()
    {
        currentExp = 0;
        initialExp += Mathf.FloorToInt(expIncreasePerLevel * currentLevel * constantA);
        expSlider.maxValue = initialExp;
        expSlider.value = currentExp;
    }
    private void RandomPowerPicker()
    {
        int countPicker = 0;
        bodyUI.gameObject.SetActive(true);
        while (countPicker < maxPowerPiker)
        {
            float delta = Random.value;
            for (int i = 0; i < bodyUI.childCount; i++)
            {
                GameObject picker = bodyUI.GetChild(i).gameObject;
                PowerPickerSO powerPickerSO = picker.GetComponentInChildren<PowerPickerSO>();
                if (delta <= powerPickerSO.GetRateUp() && picker.activeSelf == false)
                {
                    picker.SetActive(true);
                    RectTransform rectInstance = picker.GetComponent<RectTransform>();
                    rectInstance.anchorMin = minAnchor[countPicker];
                    rectInstance.anchorMax = maxAnchor[countPicker];
                    picker.GetComponent<PowerPickerUI>().SetNumberText(powerPickerSO.GetRandomNumber());          
                    countPicker++;
                    break;
                }
            }       
        }
    }
    private IEnumerator WaitToInvokePowerPicker()
    {
        yield return new WaitForSeconds(delay);
        cameraShake.Stop();
        Time.timeScale = 0f;
        RandomPowerPicker();
    }
    public void PickPower()
    {
        SetFalseAllPowerPicker();
        Time.timeScale = 1f;
        CameraShake cameraShake = FindObjectOfType<CameraShake>();
        cameraShake.ShakeDurationBack(); 
    }
    private void SetFalseAllPowerPicker()
    {
        for (int i = 0; i < bodyUI.childCount; i++)
        {
            if (bodyUI.GetChild(i).gameObject.activeSelf == true)
            {
                bodyUI.GetChild(i).gameObject.SetActive(false);
            }
        }
        bodyUI.gameObject.SetActive(false);
    }
}
