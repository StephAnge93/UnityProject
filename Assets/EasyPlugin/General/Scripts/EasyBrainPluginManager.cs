using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VoxelBusters.NativePlugins;

public class EasyBrainPluginManager : MonoBehaviour {

    public static EasyBrainPluginManager instance;
    public GameInfo gameInfo;


    // =========================================================================================================================================================================================


    private void Awake()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        if(PlayerPrefs.GetInt("firstplay") == 0){
            PlayerPrefs.SetInt("firstplay", 1);

            CreditManager.AddBalance(0, gameInfo.InitialCoinBalance);
        }
    }





    // ================================================================== Other Helping functions ==================================================================

    public bool Notification
    {
        get
        {
            return PlayerPrefs.GetInt("Notifications") == 0;
        }
        set
        {
            PlayerPrefs.SetInt("Notifications", (value) ? 0 : 1);
            AppEvents.onNotificationStatusChange?.Invoke(value);
        }
    }

    public bool IsTutorial
    {
        get
        {
            return PlayerPrefs.GetInt("isTutorial") < 3; // 0 means true
        }
        set
        {
            if (!value)
            {
                PlayerPrefs.SetInt("isTutorial", PlayerPrefs.GetInt("isTutorial") + 1);
            }
            else
            {
                PlayerPrefs.SetInt("isTutorial", 0);
            }
        }
    }

    public bool Music
    {
        get
        {
            return PlayerPrefs.GetInt("music") == 0;
        }
        set
        {
            PlayerPrefs.SetInt("music", (value) ? 0 : 1);
            AppEvents.onMusicStatusChange?.Invoke(value);
        }
    }

    public bool Sounds
    {
        get
        {
            return PlayerPrefs.GetInt("sfx") == 0;
        }
        set
        {
            PlayerPrefs.SetInt("sfx", value ? 0 : 1);
            AppEvents.onSoundStatusChange?.Invoke(value);
        }

    }

    public void Share()
    {
        ShareScreenShotUsingShareSheet();
    }

    private void ShareScreenShotUsingShareSheet()
    {
        ShareSheet _shareSheet = new ShareSheet();
        string shareText = string.Empty;

        #if UNITY_IOS
                shareText = gameInfo.ShareLink_iOS;
        #elif UNITY_ANDROID
                shareText = gameInfo.ShareLink_android;
        #endif

        _shareSheet.Text = shareText;
        
        _shareSheet.AttachScreenShot();

        NPBinding.UI.SetPopoverPointAtLastTouchPosition();
        NPBinding.Sharing.ShowView(_shareSheet, FinishedSharing);
    }

    private void FinishedSharing(eShareResult _result)
    {
        // Do something after sharing is finished.
    }

    public void OnRateClick()
    {
        string link = string.Empty;

        #if UNITY_IOS
            link = gameInfo.RateUs_iOS;
        #elif UNITY_ANDROID
            link = gameInfo.RateUs_Android;
        #endif

        Application.OpenURL(link);
    }


    // ================================================================== S O U N D    F U N C T I O N S ================================================================================================

    public AudioSource musicAud, sfxAud;
    public AudioClip click, bgMenu, bgGame;

    public void PlayMenuBackgroundMusic(){
        if(Music && (musicAud.isPlaying == false || musicAud.clip != bgMenu)){
            musicAud.Stop();
            musicAud.clip = bgMenu;
            musicAud.Play();
        }
    }

    public void PlayGameBackgroundMusic()
    {
        if (Music && (musicAud.isPlaying == false || musicAud.clip != bgGame))
        {
            musicAud.Stop();
            musicAud.clip = bgGame;
            musicAud.Play();
            Debug.Log("Playing Music");
        }
    }

    public void StopBackgroundMusic(){
        musicAud.Stop();
        Debug.Log("Stoping Music");
    }

    public void PlayClickSound(){
        if(Sounds){
            sfxAud.PlayOneShot(click);
        }
    }
}
