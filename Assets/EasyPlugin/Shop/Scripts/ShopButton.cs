using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Image itemImage;
    public Text itemNameText;
    public Button BuyButton, SelectButton, SelectedButton;
    public Text buyButtonText;

    private ShopItem_Data myItemInfo;

    private void OnEnable()
    {
        AppEvents.onAnyShopItemBought += UpdateButton;
        AppEvents.onAnyShopItemSelected += UpdateButton;
    }

    private void OnDisable()
    {
        AppEvents.onAnyShopItemBought -= UpdateButton;
        AppEvents.onAnyShopItemSelected -= UpdateButton;
    }

    public void UpdateButton(ShopItem_Data item)
    {
        SetUP(myItemInfo);
    }

    public void SetUP(ShopItem_Data item)
    {
        myItemInfo = item;
        itemImage.sprite = item.ShopAlias;
        itemNameText.text = item.name;
        buyButtonText.text = "Buy $" + item.Price.ToString();

        if (myItemInfo.IsSelected())
        {
            SelectedButton.gameObject.SetActive(true);
            BuyButton.gameObject.SetActive(false);
            SelectButton.gameObject.SetActive(false);
        }else if (myItemInfo.IsOwned())
        {
            SelectedButton.gameObject.SetActive(false);
            BuyButton.gameObject.SetActive(false);
            SelectButton.gameObject.SetActive(true);
        }
        else
        {
            SelectedButton.gameObject.SetActive(false);
            BuyButton.gameObject.SetActive(true);
            SelectButton.gameObject.SetActive(false);
        }
    }

    public void OnBuyClick()
    {
        if(myItemInfo != null)
        {
            if(CreditManager.GetBalance(myItemInfo.CurrencyId) > myItemInfo.Price)
            {
                myItemInfo.Buy();
                myItemInfo.Select();
            }
            else
            {
                Debug.LogWarning("Not enought coins");
            }
        }
    }

    public void OnSelectClick()
    {
        if(myItemInfo != null)
        {
            myItemInfo.Select();
        }
    }
}
