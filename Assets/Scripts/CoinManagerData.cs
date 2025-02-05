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
        PlayerPrefs.SetInt(CoinKey, coin);
        this.coin = PlayerPrefs.GetInt(CoinKey);
    }
    public int GetCoinManager()
    {
        return coin;
    }
    public void SetExpManager(int exp)
    {
        PlayerPrefs.SetInt(ExpKey, exp);
        this.exp = PlayerPrefs.GetInt(ExpKey);
    }
    public int GetExpManager()
    {
        return exp;
    }
}
