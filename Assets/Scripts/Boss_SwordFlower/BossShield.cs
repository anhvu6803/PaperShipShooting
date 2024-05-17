using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] private PowerShield powerShield;
    [SerializeField] private Transform largeSwordContainer;
    [SerializeField] private Transform smallSwordContainer;
    [SerializeField] private Health healthObject;
    private void Start()
    {
        if (powerShield != null)
        {
            powerShield.UseShield();
        }
    }
    private void Update()
    {
        if(largeSwordContainer.childCount == 0 && smallSwordContainer.childCount == 0) 
        {
            powerShield.TakeDamageShield(powerShield.GetMaxHealth());
            healthObject.SetHasShield(false);
        }
        else
        {
            healthObject.SetHasShield(true);
        }
    }
}
