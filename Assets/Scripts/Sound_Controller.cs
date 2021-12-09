using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Controller : MonoBehaviour
{
    public static Sound_Controller Instance;

    public AudioSource[] Audio_Sources;
    public AudioClip Btn_Click;
    public AudioClip Coin_Clip;
    public AudioClip Fruit_Clip;
    public AudioClip Clap_Clip;
    public AudioClip Wall_Touch_Clip;
    public AudioClip Level_Complete_Clip;
    public AudioClip Gameover_Clip;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Audio_Sources = GetComponents<AudioSource>();
    }

    public void Play_Sound(int i, AudioClip clip)
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            if (clip == Gameover_Clip)
            {
                FindObjectOfType<Game_Controller>().GetComponent<AudioSource>().Stop();
            }
            Audio_Sources[i].clip = clip;
            Audio_Sources[i].loop = false;
            Audio_Sources[i].Play();
        }
    }
}
