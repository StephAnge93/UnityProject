using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Data/Shop/Inventory")]
public class ShopInventory : ScriptableObject
{
    public List<ShopItem_Data> items = new List<ShopItem_Data>();
}
