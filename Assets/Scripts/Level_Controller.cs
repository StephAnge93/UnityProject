using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Controller : MonoBehaviour
{
    public List<GameObject> All_Levels = new List<GameObject>();

    private void OnEnable()
    {
        PlayerPrefs.SetInt("Level_Lock_" + 1, 1);

        for (int i = 1; i <= All_Levels.Count; i++)
        {
            if (PlayerPrefs.GetInt("Level_Lock_" + i, 0) == 1)
            {
                print("Unlock = " + i);
                All_Levels[i - 1].transform.GetChild(1).gameObject.SetActive(false);
                if (PlayerPrefs.GetInt("Level_Complete_" + i, 0) == 1)
                {
                    print("Commpleted = " + i);
                    All_Levels[i - 1].transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    print("Remain = " + i);
                    All_Levels[i - 1].transform.GetChild(2).gameObject.SetActive(false);
                }
            }
            else
            {
                print("Locked = " + i);
                All_Levels[i - 1].transform.GetChild(1).gameObject.SetActive(true);
                All_Levels[i - 1].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void Select_Level()
    {
        Sound_Controller.Instance.Play_Sound(0,Sound_Controller.Instance.Btn_Click);
        int i = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        if(PlayerPrefs.GetInt("Level_Lock_" + i, 0) == 1)
        {
            PlayerPrefs.SetInt("Cureent_Level", i);
            SceneManager.LoadSceneAsync("PlayScene");
        }
    }
}
