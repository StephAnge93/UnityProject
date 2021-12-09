using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home_Controller : MonoBehaviour
{
    public static Home_Controller Instance;

    public Text Fruits_txt;
    public Text Coins_txt;
    public GameObject Sound_off;
    public GameObject Home_Panel;
    public GameObject Level_Panel;
    public GameObject Shop_Panel;
    public List<GameObject> Bats = new List<GameObject>(); 

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        if (PlayerPrefs.GetInt("first", 0) == 0)
        {
            PlayerPrefs.SetInt("first", 1);
            PlayerPrefs.SetInt("Total_Fruits", 0);
            PlayerPrefs.SetInt("Total_Coins", 50);
            PlayerPrefs.SetInt("Cureent_Bat", 1);
            PlayerPrefs.SetInt("Cureent_Level", 1);
        }

        Show_Current_Bat();
        Set_Fruit_Txt();
        Set_Coin_Txt();

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            Sound_off.SetActive(true);
            GetComponent<AudioSource>().Play();
        }
    }

    public void Show_Current_Bat()
    {
        foreach(GameObject obj in Bats)
        {
            obj.SetActive(false);
        }
        Bats[PlayerPrefs.GetInt("Cureent_Bat", 1) - 1].SetActive(true);
    }

    public void Set_Fruit_Txt()
    {
        if (PlayerPrefs.GetInt("Total_Fruits", 0) < 1000)
        {
            Fruits_txt.text = PlayerPrefs.GetInt("Total_Fruits", 0).ToString("00");
        }
        else
        {
            Fruits_txt.text = ((int)PlayerPrefs.GetInt("Total_Fruits", 0) / 1000).ToString() + "K";
        }
    }

    public void Set_Coin_Txt()
    {
        if (PlayerPrefs.GetInt("Total_Coins", 0) < 1000)
        {
            Coins_txt.text = PlayerPrefs.GetInt("Total_Coins", 0).ToString("00");
        }
        else
        {
            Coins_txt.text = ((int)PlayerPrefs.GetInt("Total_Coins", 0) / 1000).ToString() + "K";
        }
    }

    public void Play()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        SceneManager.LoadSceneAsync("PlayScene");
    }

    public void Open_Level()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        StartCoroutine(Fade_In(Level_Panel));
        StartCoroutine(Fade_Out(Home_Panel));
        Level_Panel.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        Level_Panel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
            StartCoroutine(Fade_Out(Level_Panel));
            StartCoroutine(Fade_In(Home_Panel));
        });
    }

    public void Open_Shop()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        StartCoroutine(Fade_In(Shop_Panel));
        StartCoroutine(Fade_Out(Home_Panel));
        Shop_Panel.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
        Shop_Panel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
            StartCoroutine(Fade_Out(Shop_Panel));
            StartCoroutine(Fade_In(Home_Panel));
        });
    }

    IEnumerator Fade_In(GameObject obj)
    {
        obj.SetActive(true);
        //float a = 0;
        //while (a < 1)
        //{
        //    a += Time.deltaTime * 3;
        //    obj.GetComponent<CanvasGroup>().alpha = a;
        //    yield return null;
        //}
        yield return null;
    }

    IEnumerator Fade_Out(GameObject obj)
    {
        //float a = 1;
        //while (a > 0)
        //{
        //    a -= Time.deltaTime * 3;
        //    obj.GetComponent<CanvasGroup>().alpha = a;
        //    yield return null;
        //}
        obj.SetActive(false);
        yield return null;
    }

    public void Sound_On_Off()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
            Sound_off.SetActive(false);
            GetComponent<AudioSource>().Stop();
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            Sound_off.SetActive(true);
            GetComponent<AudioSource>().Play();
        }
    }

    public void Exit_Game()
    {
        Application.Quit();
    }

    public void Open_LeaderBoard()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        Social.ShowLeaderboardUI();
    }

    public void Rate_Game()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        EasyBrainPluginManager.instance.OnRateClick();
    }

    public void Share_Game()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        EasyBrainPluginManager.instance.Share();
    }

}
