using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPlateScript : MonoBehaviour
{
    public Text plate;

    private void OnEnable()
    {
        AppEvents.onCoinBalanceChange += UpdateText;
    }

    private void OnDisable()
    {
        AppEvents.onCoinBalanceChange -= UpdateText;
    }

    private void Start()
    {
        UpdateText(CreditManager.GetBalance(0));
    }

    public void UpdateText(int coinBalance)
    {
        plate.text = coinBalance.ToString();
    }
}
