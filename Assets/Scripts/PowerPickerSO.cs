using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PickerType { health, damage, bullet, speed, recovery }
public class PowerPickerSO : MonoBehaviour
{
    [SerializeField] private PickerType pickerType;
    [SerializeField] private int initialNumber;
    [SerializeField] private float rateUp;
    [SerializeField] private int numberMagnitude;
    private int numberIncrease;
    public PickerType GetPickerType() 
    { 
        return pickerType; 
    }
    public int GetNumberIncrease()
    {
        return numberIncrease;
    }
    public int GetRandomNumber()
    {
        return numberIncrease = Random.Range(initialNumber, initialNumber + numberMagnitude);
    }
    public float GetRateUp()
    {
        return rateUp;
    }
}
