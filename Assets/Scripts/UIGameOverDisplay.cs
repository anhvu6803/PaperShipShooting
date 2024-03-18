using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverDisplay : MonoBehaviour
{
    [SerializeField] GameObject iconObject;
    // y * adjustConst = equationConstA * x / equationConstB;
    [SerializeField] private float adjustConst;
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float speedScore;
    private ScoreKeeper scoreKeeper;
    private float maxScore;
    private float currentScore;
    [Header("Coin")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject coinObject;
    [SerializeField] private float maxCoinColorA;
    [SerializeField] private float speedCoinColorA;
    [SerializeField] private float equationCoinA;
    [SerializeField] private float equationCoinB;
    [SerializeField] private float maxCoinEarn;
    private Image coinImage;
    private float speedCoin;
    private float maxCoin;
    private float currentCoin;
    [Header("Exp")]
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private GameObject expObject;
    [SerializeField] private float maxColorA;
    [SerializeField] private float speedColorA;
    [SerializeField] private float equationExpA;
    [SerializeField] private float equationExpB;
    [SerializeField] private float maxExpEarn;
    private Image expImage;
    private float speedExp;
    private float maxExp;
    private float currentExp;
    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    } 
    private void Start()
    {
        InitialText();
        InitialComponetScore();
        InitialComponetExp();
        InitialComponetCoin();
    }
    void Update()
    {
        RunScoreText();
        if (Mathf.FloorToInt(currentScore) == Mathf.FloorToInt(maxScore))
        {
            RunColor(expImage, maxColorA, expObject.transform);
            RunColor(coinImage, maxCoinColorA, coinObject.transform);
        }
        RunExp();
        RunCoin();
        if (Mathf.FloorToInt(currentCoin) == Mathf.FloorToInt(maxCoin)
            && Mathf.FloorToInt(currentExp) == Mathf.FloorToInt(maxExp))
        {
            ActiveIcon();
        }
    }
    private void ActiveIcon()
    {
        iconObject.SetActive(true);
        foreach(Transform icon in iconObject.transform)
        {
            RunColor(icon.GetComponent<Image>(), 1);
        }
    }
    private void InitialText()
    {
        scoreText.text = "0000000000";
        coinText.text = "0";
        expText.text = "0";
    }
    private void InitialComponetScore()
    {
        currentScore = 0;
        maxScore = scoreKeeper.GetScore();
        speedScore = SetSpeed(maxScore);
    }
    private void InitialComponetCoin()
    {
        currentCoin = 0;
        coinImage = coinObject.GetComponent<Image>();
        InitialColor(coinImage);
        maxCoinColorA = 0.7f;
        speedCoinColorA = maxCoinColorA;
        CalculateMaxCoin();
        speedCoin = SetSpeed(maxCoin);
    }
    private void InitialComponetExp()
    {
        currentExp = 0;
        expImage = expObject.GetComponent<Image>();
        InitialColor(expImage);
        maxColorA = 0.7f;
        speedColorA = maxColorA;
        CalculateMaxExp();
        speedExp = SetSpeed(maxExp);
    }
    private float SetSpeed(float maxCount)
    {
        if(maxCount < 10)
        {
            return maxCount * 100;
        }
        else if(maxCount > 10000)
        {
            return maxCount;
        }
        return maxCount * 10;
    }
    private void RunScoreText()
    {    
        if(currentScore < maxScore)
        {
            currentScore += Time.deltaTime * speedScore;
            currentScore = Mathf.Clamp(currentScore, 10, maxScore);
            scoreText.text = scoreKeeper.EditScore(Mathf.FloorToInt(currentScore));
        }
    }
    private void RunExp()
    {
        if(currentExp < maxExp && expObject.transform.GetChild(1).gameObject.activeSelf == true) 
        {
            currentExp += Time.deltaTime * speedExp;
            Debug.Log(currentExp);
            currentExp = Mathf.Clamp(Mathf.FloorToInt(currentExp), 0, maxExp);

            expText.text = currentExp.ToString();
        }
    }
    private void CalculateMaxExp()
    {
        float tempExp = equationExpA * maxScore;
        if (maxScore <= 99999 && maxScore > 9999)
        {
            equationExpB = 300;
        }
        else if(maxScore > 99999)
        {
            equationExpB = 100;
        }
        else
        {
            equationExpB = 500;
        }
        maxExp = Mathf.FloorToInt((tempExp * adjustConst) / equationExpB);
        maxExp = Mathf.Clamp(maxExp, 0, maxExpEarn);
        Debug.Log(maxExp);
    }
    private void RunCoin()
    {
        if (currentCoin < maxCoin && coinObject.transform.GetChild(1).gameObject.activeSelf == true)
        {
            currentCoin += Time.deltaTime * speedCoin;
            currentCoin = Mathf.Clamp(Mathf.FloorToInt(currentCoin), 1, maxCoin);
            Debug.Log(currentCoin);
            coinText.text = currentCoin.ToString();
        }
    }
    private void CalculateMaxCoin()
    {
        float tempExp = equationCoinA * maxScore;
        if (maxScore <= 99999 && maxScore > 9999)
        {
            equationCoinB = 2;
        }
        else if (maxScore > 99999)
        {
            equationCoinB = 0.5f;
        }
        else
        {
            equationCoinB = 3;
        }
        maxCoin = Mathf.FloorToInt((tempExp * adjustConst) / equationCoinB);
        maxCoin = Mathf.Clamp(maxCoin, 1, maxCoinEarn);
        Debug.Log(maxCoin);
    }
    private void RunColor(Image image, float colorChange, Transform parent = null)
    {
        Color color = image.color;
        if(color.a < colorChange)
        {
            color.a += Time.deltaTime * speedColorA;
            color.a = Mathf.Clamp(color.a, 0, colorChange);
            image.color = color;
            if(color.a == colorChange && parent != null)
            {
                foreach(Transform child in parent)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }      
    }
    private void InitialColor(Image image)
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;
    }
}
