using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsPanelScript : MonoBehaviour
{
    public static SettingsPanelScript instance;

    public GameObject MainPanel;

    private static string SoundsOnText = "Sounds: <color=green>ON</color>";
    private static string SoundsOffText = "Sounds: <color=red>Off</color>";
    private static string MusicOnText = "Music: <color=green>ON</color>";
    private static string MusicOffText = "Music: <color=red>Off</color>";
    private static string NotificationOnText = "Notifications: <color=green>ON</color>";
    private static string NotificationOffText = "Notifications: <color=red>Off</color>";

    public Text SoundText, MusicText, NotificationText;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        SetUpButtons();
    }

    public void SetUpButtons() {
        NotificationText.text = EasyBrainPluginManager.instance.Notification? NotificationOnText:NotificationOffText;
        SoundText.text = EasyBrainPluginManager.instance.Sounds ? SoundsOnText : SoundsOffText;
        MusicText.text = EasyBrainPluginManager.instance.Music ? MusicOnText : MusicOffText;
    }

    public void OnSoundClick() {
        EasyBrainPluginManager.instance.Sounds = !EasyBrainPluginManager.instance.Sounds;
        SoundText.text = EasyBrainPluginManager.instance.Sounds ? SoundsOnText : SoundsOffText;
        EasyBrainPluginManager.instance.PlayClickSound();
    }

    public void OnMusicClick() {
        EasyBrainPluginManager.instance.PlayClickSound();
        EasyBrainPluginManager.instance.Music = !EasyBrainPluginManager.instance.Music;
        MusicText.text = EasyBrainPluginManager.instance.Music ? MusicOnText : MusicOffText;

        if (EasyBrainPluginManager.instance.Music)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Gameplay"))
            {
                EasyBrainPluginManager.instance.PlayGameBackgroundMusic();
            }
            else
            {
                EasyBrainPluginManager.instance.PlayMenuBackgroundMusic();
            }
        }
        else
        {
            EasyBrainPluginManager.instance.StopBackgroundMusic();
        }
    }

    public void OnNotificationClick() {
        EasyBrainPluginManager.instance.PlayClickSound();
        EasyBrainPluginManager.instance.Notification = !EasyBrainPluginManager.instance.Notification;
        NotificationText.text = EasyBrainPluginManager.instance.Notification ? NotificationOnText : NotificationOffText;

        if (!EasyBrainPluginManager.instance.Notification)
        {
            NotificationManager.instance.RemoveAllPreviousNotifications();
        }
        else
        {
            NotificationManager.instance.SetUpNextNotification();
        }
    }
}
