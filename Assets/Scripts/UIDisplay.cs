using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    private static UIDisplay instance;
    [Header("Health")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private Health health;
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;
    [Header("Power")]
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private TextMeshProUGUI ultimateText;
    [SerializeField] private Button ultimateButton;
    [SerializeField] private Button shieldButton;
    private StorePower storePower;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        storePower = FindObjectOfType<StorePower>();
    }
    void Start()
    {
        healthBar.maxValue = health.GetMaxHealth();
    }
    void Update()
    {
        healthBar.value = health.GetHealth();
        scoreText.text = scoreKeeper.EditScore(scoreKeeper.GetScore());
        AdjustPowerUI();
    }
    public void ModifyMaxHealthBar()
    {
        healthBar.maxValue = health.GetMaxHealth();
    }
    private void AdjustPowerUI()
    {
        ChangeColor(shieldText, shieldButton, storePower.GetShieldCount());
        ChangeColor(ultimateText, ultimateButton, storePower.GetUltimateCount());
        shieldText.text = "x" + storePower.GetShieldCount().ToString();
        ultimateText.text = "x" + storePower.GetUltimateCount().ToString();
    }
    private void ChangeColor(TextMeshProUGUI text, Button button, int count)
    {
        Color colorT = text.color;
        if (count <= 0)
        {
            button.interactable = false;
            colorT.a = 0.5f;
        }
        else
        {
            button.interactable = true;
            colorT.a = 1;
        }
        text.color = colorT;
    }
}
