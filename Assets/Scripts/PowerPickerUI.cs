using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerPickerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    public void SetNumberText(int number)
    {
        numberText.text = "+" + number.ToString();
    }
}
