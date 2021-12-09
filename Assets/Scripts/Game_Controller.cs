using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    public static Game_Controller Instance;

    public Text Fruits_txt;
    public Text Coins_txt;
    public Text Level_txt;
    public GameObject Instruction_Panel;
    public GameObject Pause_Panel;
    public GameObject Level_Complete_Panel;
    public GameObject Level_Fail_Panel;
    public List<GameObject> Bats = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //Show_Current_Bat();
        Level_txt.text = PlayerPrefs.GetInt("Cureent_Level", 1).ToString();
        Set_Fruit_Txt();
        Set_Coin_Txt();

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            GetComponent<AudioSource>().Play();
        }
        //PlayerPrefs.SetInt("show_instruction", 0);
        Open_Instruction();
    }

    void Open_Instruction()
    {
        if(PlayerPrefs.GetInt("show_instruction", 0) == 0)
        {
            PlayerPrefs.SetInt("show_instruction", 1);
            Instruction_Panel.SetActive(true);
        }
    }

    public void Show_Current_Bat()
    {
        foreach (GameObject obj in Bats)
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

    public void Pause_Game()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        Pause_Panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume_Game()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        Pause_Panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Retry_Game()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void Back_to_Home()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("HomeScene");
    }

    public void Next()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        if (PlayerPrefs.GetInt("Cureent_Level", 1) < 10)
        {
            PlayerPrefs.SetInt("Cureent_Level", PlayerPrefs.GetInt("Cureent_Level", 1) + 1);
        }
        else
        {
            PlayerPrefs.SetInt("Cureent_Level", 1);
        }
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void Open_Level_Complete()
    {
        PlayerPrefs.SetInt("Level_Complete_" + PlayerPrefs.GetInt("Cureent_Level", 1), 1);

        print(PlayerPrefs.GetInt("Level_Lock_" + (PlayerPrefs.GetInt("Cureent_Level", 1) + 1), 0));
        if (PlayerPrefs.GetInt("Level_Lock_" + (PlayerPrefs.GetInt("Cureent_Level", 1) + 1), 0) == 0)
        {
            print(" jay 6..");
            PlayerPrefs.SetInt("Level_Lock_" + (PlayerPrefs.GetInt("Cureent_Level", 1) + 1), 1);
        }
        print("Current_Level = " + PlayerPrefs.GetInt("Cureent_Level", 1)); 

        StartCoroutine(Open_Level_Complete_1());
    }
    IEnumerator Open_Level_Complete_1()
    {
        yield return new WaitForSeconds(2);
        Level_Complete_Panel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = FindObjectOfType<Bat_Controller>().Fruits.ToString();
        Level_Complete_Panel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = FindObjectOfType<Bat_Controller>().Coins.ToString();
        Level_Complete_Panel.SetActive(true);
    }

    public void Open_Level_Fail()
    {
        StartCoroutine(Open_Level_Fail_1());
    }
    IEnumerator Open_Level_Fail_1()
    {
        yield return new WaitForSeconds(2);
        Level_Fail_Panel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = FindObjectOfType<Bat_Controller>().Fruits.ToString();
        Level_Fail_Panel.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>().text = FindObjectOfType<Bat_Controller>().Coins.ToString();
        Level_Fail_Panel.SetActive(true);
    }

    public void HideInstructionPanel()
    {
        Instruction_Panel.SetActive(false);
    }
}
