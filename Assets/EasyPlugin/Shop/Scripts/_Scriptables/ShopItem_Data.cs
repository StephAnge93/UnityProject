using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Shop/Item")]
public class ShopItem_Data : ScriptableObject
{
    public int Id;

    /// <summary>
    /// If <see langword="true"/> then this item will be unlocked when user starts the app for the first time;
    /// </summary>
    public bool UnlockAtStart;

    /// <summary>
    /// If true then this item will be selected by default when user runs the game for the first time.
    /// </summary>
    public bool SelectAtStart;

    /// <summary>
    /// Price of item.
    /// </summary>
    public int Price;
    /// <summary>
    /// Currency Id with which this item can be bought, if you only have one currecny in the game ignore this field.
    /// </summary>
    public int CurrencyId;

    /// <summary>
    /// Name of the item which will be shown in the shop.
    /// </summary>
    public string ItemName;

    /// <summary>
    /// Any prefab that you want to associate with this item.
    /// </summary>
    public GameObject Prefab;

    /// <summary>
    /// This is the image which we will show on the shop to represent this item.
    /// </summary>
    public Sprite ShopAlias;

    public void Buy()
    {
        if(CreditManager.GetBalance(CurrencyId) >= Price)
        {
            CreditManager.AddBalance(CurrencyId, -Price);
            OwnThisItem();
        }
    }

    public bool CanBuy()
    {
        return CreditManager.GetBalance(CurrencyId) >= Price;
    }

    public void OwnThisItem()
    {
        PlayerPrefs.SetInt("ShopItem_" + Id , 1);
        AppEvents.onAnyShopItemBought?.Invoke(this);
    }

    public bool IsOwned()
    {
        return PlayerPrefs.GetInt("ShopItem_" + Id) == 1;
    }

    public bool IsSelected()
    {
        return PlayerPrefs.GetInt("ShopItem_Selected") == Id;
    }

    public void Select()
    {
        PlayerPrefs.SetInt("ShopItem_Selected", Id);
        AppEvents.onAnyShopItemSelected?.Invoke(this);
    }
}
