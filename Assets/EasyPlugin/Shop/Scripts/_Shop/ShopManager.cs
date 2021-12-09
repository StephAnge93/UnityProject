using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public ShopInventory inventory;

    public bool IsFirstSetUp {
        get {
            return PlayerPrefs.GetInt("ShopSetup") == 0;
        }
        set {
            PlayerPrefs.SetInt("ShopSetup", (value) ? 0 : 1);
        }
    }

    private void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Initialise();
   }

    private void Initialise()
    {
        if (IsFirstSetUp) {
            foreach (var item in inventory.items)
            {
                if (item.UnlockAtStart) {
                    item.OwnThisItem();
                }

                if (item.SelectAtStart) {
                    item.Select();
                }
            }

            IsFirstSetUp = false;
        }
    }
}
