using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop_Controller : MonoBehaviour
{
    public Text Coins_txt;
    public Sprite Selected_Sprite;
    public Sprite UnSelected_Sprite;
    public List<GameObject> All_Bats = new List<GameObject>();
    int[] Prices = { 0, 200, 300, 400 };

    private void OnEnable()
    {
        PlayerPrefs.SetInt("Bat_Lock_" + 1, 1);
        Bat_Status();
    }

    void Bat_Status()
    {
        if (PlayerPrefs.GetInt("Total_Coins", 0) < 1000)
        {
            Coins_txt.text = PlayerPrefs.GetInt("Total_Coins", 0).ToString("00");
        }
        else
        {
            Coins_txt.text = ((int)PlayerPrefs.GetInt("Total_Coins", 0) / 1000).ToString() + "K";
        }

        for (int i = 1; i <= All_Bats.Count; i++)
        {
            if (PlayerPrefs.GetInt("Bat_Lock_" + i, 0) == 1)
            {
                All_Bats[i - 1].transform.GetChild(2).gameObject.SetActive(false);
                if (PlayerPrefs.GetInt("Cureent_Bat", 1) == i)
                {
                    All_Bats[i - 1].transform.GetComponent<Image>().sprite = Selected_Sprite;
                }
                else
                {
                    All_Bats[i - 1].transform.GetComponent<Image>().sprite = UnSelected_Sprite;
                }
            }
            else
            {
                All_Bats[i - 1].transform.GetChild(2).gameObject.SetActive(true);
                All_Bats[i - 1].transform.GetComponent<Image>().sprite = UnSelected_Sprite;
            }
        }
    }

    public void Select_Bat()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        int i = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        if (PlayerPrefs.GetInt("Bat_Lock_" + i, 0) == 0)
        {
            if (PlayerPrefs.GetInt("Total_Coins", 0) >= Prices[i-1])
            {
                PlayerPrefs.SetInt("Total_Coins", PlayerPrefs.GetInt("Total_Coins", 0) - Prices[i - 1]);
                PlayerPrefs.SetInt("Bat_Lock_" + i, 1);
                PlayerPrefs.SetInt("Cureent_Bat", i);
                Bat_Status();
                Home_Controller.Instance.Set_Coin_Txt();
                Home_Controller.Instance.Show_Current_Bat();
            }
        }
        else
        {
            PlayerPrefs.SetInt("Cureent_Bat", i);
            Home_Controller.Instance.Show_Current_Bat();
        }
    }
}
