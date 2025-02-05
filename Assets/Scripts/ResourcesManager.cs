using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private CoinManagerData coinManager;

    private void Start()
    {
        coinText.text = coinManager.GetCoinManager().ToString();
        expText.text = coinManager.GetExpManager().ToString();
    }

}
