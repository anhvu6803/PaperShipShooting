using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Power Rate Up", fileName = "New Power Rate Up")]
public class PowerAndRateUpSO : ScriptableObject
{
    [SerializeField] private GameObject powerPrefab;
    [SerializeField] private float powerRateUp;
    [SerializeField] private float speed;
    public float GetPowerRateUp()
    {
        return powerRateUp;
    }
    public GameObject GetPower()
    {
        return powerPrefab;
    }
    public float GetSpeed()
    {
        return speed;
    }
}
