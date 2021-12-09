using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelScript : MonoBehaviour
{
    public Transform GridParent;
    public GameObject ShopItemPrefab;

    private void Start()
    {
        Invoke("ShowShop", 0.4f);
    }

    public void ShowShop()
    {
        foreach (var item in ShopManager.instance.inventory.items)
        {
            GameObject g = Instantiate(ShopItemPrefab, GridParent) as GameObject;
            g.GetComponent<ShopButton>().SetUP(item);
        }
    }
}
