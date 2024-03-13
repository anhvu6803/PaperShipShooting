using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPicker : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private UIDisplay displayUI;
    private Health playerHealth;
    private Player playerState;
    private DamageDealer bulletDamage;
    private Shooter playerShooter;
    private void Start()
    {
        playerHealth = player.GetComponent<Health>();
        playerState = player.GetComponent<Player>();
        bulletDamage = playerBullet.GetComponent<DamageDealer>();
        playerShooter = player.GetComponent<Shooter>();
    }
    public void PickPower()
    {
        PowerPickerSO pickerSO = transform.GetComponentInChildren<PowerPickerSO>();
        ApplyPowerPicker(pickerSO);
    }
    private void ApplyPowerPicker(PowerPickerSO pickerSO)
    {
        switch (pickerSO.GetPickerType())
        {
            case PickerType.health:
                playerHealth.ModifyMaxHealth(pickerSO.GetNumberIncrease());
                displayUI.ModifyMaxHealthBar();
                break;
            case PickerType.recovery:
                playerHealth.ModifyHealth(pickerSO.GetNumberIncrease());
                break;
            case PickerType.damage:
                bulletDamage.ModifyDamage(pickerSO.GetNumberIncrease());
                break;
            case PickerType.speed:
                playerState.ModifySpeed(pickerSO.GetNumberIncrease());
                break;
            case PickerType.bullet:
                playerShooter.ModifyNumberBullet(pickerSO.GetNumberIncrease());
                break;
        }
    }
}
