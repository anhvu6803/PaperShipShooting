using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Coin Data", fileName = "New Coin Data")]
public class CoinManagerData : ScriptableObject
{
    [SerializeField] private int coin;
    [SerializeField] private int exp;

    public static string CoinKey = "CoinManager";
    public static string ExpKey = "ExpManager";

    public void SetCoinManager(int coin)
    {
        this.coin = PlayerPrefs.GetInt(CoinKey);
        PlayerPrefs.SetInt(CoinKey, this.coin + coin);      
    }
    public int GetCoinManager()
    {
        return PlayerPrefs.GetInt(CoinKey);
    }
    public void SetExpManager(int exp)
    {
        this.exp = PlayerPrefs.GetInt(ExpKey);
        PlayerPrefs.SetInt(ExpKey, this.exp + exp);
    }
    public int GetExpManager()
    {
        return PlayerPrefs.GetInt(ExpKey);
    }
}
