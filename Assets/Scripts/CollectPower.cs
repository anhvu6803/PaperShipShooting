using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType { LevelUp, Shield, Ultimate }
public class CollectPower : MonoBehaviour
{
    [SerializeField] private PowerType powerType;
    public PowerType GetPowerType()
    { 
        return powerType; 
    }
}
