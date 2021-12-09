using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnCoinBalanceChange(int newBalance);
public delegate void OnSoundStatusChange(bool isSoundOn);
public delegate void OnMusicStatusChange(bool isMusicOn);
public delegate void OnNotificationStatusChange(bool isNotificationsOn);

public delegate void OnAnyShopItemBought(ShopItem_Data item);
public delegate void OnAnyShopItemSelected(ShopItem_Data item);

public class AppEvents : MonoBehaviour
{
    public static OnCoinBalanceChange onCoinBalanceChange;
    public static OnSoundStatusChange onSoundStatusChange;
    public static OnMusicStatusChange onMusicStatusChange;
    public static OnNotificationStatusChange onNotificationStatusChange;

    public static OnAnyShopItemBought onAnyShopItemBought;
    public static OnAnyShopItemSelected onAnyShopItemSelected;
}
