using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePower : MonoBehaviour
{
    [SerializeField] private int shieldCount = 0;
    [SerializeField] private int ultimateCount = 0;
    private static StorePower instance;
    private void Awake()
    {
        ManageStorePowerSingleton();
    }
    private void ManageStorePowerSingleton()
    {
        if (instance != null)
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
    public void ResetPower()
    {
        shieldCount = 0;
        ultimateCount = 0;
    }
    public int GetShieldCount()
    {
        return shieldCount;
    }
    public void IncreaseShieldCount()
    {
        if (shieldCount < 99)
            shieldCount++;
    }
    public void DecreaseShieldCount()
    {
        if (shieldCount > 0)
            shieldCount--;
    }
    public int GetUltimateCount()
    {
        return ultimateCount;
    }
    public void IncreaseUltimateCount()
    {
        if (ultimateCount < 99)
            ultimateCount++;
    }
    public void DecreaseUltimateCount()
    {
        if(ultimateCount > 0)
            ultimateCount--;
    }
}
