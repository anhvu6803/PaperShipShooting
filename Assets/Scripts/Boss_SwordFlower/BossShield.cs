using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] private PowerShield powerShield;
    [SerializeField] private Transform largeSwordContainer;
    [SerializeField] private Transform smallSwordContainer;
    [SerializeField] private Health healthObject;
    [SerializeField] private AnimationClip breakShieldClip;
    [SerializeField] private Component[] components;
    public event Action onShieldBreak;
    public event Action<bool> _onShieldBreak;
    private void Start()
    {
        if (powerShield != null)
        {
            powerShield.UseShield();
        }
    }
    private void Update()
    {
        if(largeSwordContainer.childCount == 0 && smallSwordContainer.childCount == 0 && this.enabled) 
        {
            powerShield.TakeDamageShield(powerShield.GetMaxHealth());
            healthObject.SetHasShield(false);
            StartCoroutine(DisableAllComponent());
            onShieldBreak();
            _onShieldBreak(false);
        }
        else
        {
            healthObject.SetHasShield(true);
        }
    }
    private IEnumerator DisableAllComponent()
    {
        foreach (Component component in components)
        {
            if (component is Behaviour)
            {
                ((Behaviour)component).enabled = false;
            }
        }
        yield return new WaitForSeconds(breakShieldClip.length);
        foreach (Component component in components)
        {
            if (component is Behaviour)
            {
                ((Behaviour)component).enabled = true;
            }
        }
        this.enabled = false;
    }
}
