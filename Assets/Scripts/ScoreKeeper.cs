using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private int score = 0;
    [SerializeField] PowerLevelUp playerLevel;
    private static ScoreKeeper instance;
    private void Awake()
    {
        ManageScoreSingleton();
    }
    private void ManageScoreSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }
    public void ModifyScore(int value)
    {
        if (score < 999999999)
        {
            score += value;
            if(playerLevel != null)
            {
                score *= playerLevel.GetLevel();
            }
        }
        Mathf.Clamp(score, 0, 999999999);
        
    }
    public void ResetScore()
    {
        score = 0;
    }
    public string EditScore(int value)
    {
        int scoreLength = value.ToString().Length;
        string strScore = "";
        if (scoreLength < 9)
        {
            for (int i = 9; i > scoreLength; i--)
            {
                strScore += "0";
            }
            strScore += value.ToString();
        }
        else
        {
            if (value < 999999999)
                strScore += value.ToString();
            else
                strScore = "999999999";
        }
        return strScore;
    }
}
