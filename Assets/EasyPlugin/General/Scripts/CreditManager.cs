using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreditManager {

    public static int GetBalance(int currency_id){
        return PlayerPrefs.GetInt("coin_" + currency_id);
    }

    public static void AddBalance(int currency_id, int amount){
        int newAmount = GetBalance(currency_id) + amount;
        PlayerPrefs.SetInt("coin_" + currency_id, newAmount);
        AppEvents.onCoinBalanceChange?.Invoke(newAmount);
    }
}
