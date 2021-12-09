using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(EasyBrainPluginManager.instance.Notification && string.IsNullOrEmpty(NotificationId))
        {
            SetUpNextNotification();
        }
    }

    public string NotificationId
    {
        get
        {
            return PlayerPrefs.GetString("NotificaionId");
        }
        set
        {
            PlayerPrefs.SetString("NotificaionId", value);
        }
    }

    public void SetUpNextNotification()
    {
        Debug.LogWarning("Setting up new notification");
        RemoveAllPreviousNotifications();
        CrossPlatformNotification _notification = CreateNotification(86400, eNotificationRepeatInterval.DAY);

        //Schedule this local notification.
        NotificationId = NPBinding.NotificationService.ScheduleLocalNotification(_notification);
    }

    public void RemoveAllPreviousNotifications()
    {
        Debug.LogWarning("Removing all notification");
        NPBinding.NotificationService.CancelAllLocalNotification();
    }

    private CrossPlatformNotification CreateNotification(long _fireAfterSec, eNotificationRepeatInterval _repeatInterval)
    {
        // User info - Is used to set custom data. Create a dictionary and set your data if any.
        IDictionary _userInfo = new Dictionary<string, string>();
        _userInfo["data"] = "add what is required";

        // Set iOS specific properties
        CrossPlatformNotification.iOSSpecificProperties _iosProperties = new CrossPlatformNotification.iOSSpecificProperties();
        _iosProperties.HasAction = true;
        _iosProperties.AlertAction = "Smash !!";

        // Set Android specific properties
        CrossPlatformNotification.AndroidSpecificProperties _androidProperties = new CrossPlatformNotification.AndroidSpecificProperties();
        _androidProperties.ContentTitle = "Start your Puzzle adventure now";
        _androidProperties.TickerText = "Start your next adventure now !! Break your last record";
        _androidProperties.LargeIcon = "icon.png"; //Keep the files in Assets/PluginResources/VoxelBusters/NativePlugins/Android folder.

        // Create CrossPlatformNotification instance
        CrossPlatformNotification _notification = new CrossPlatformNotification();
        _notification.AlertBody = "Start your next adventure now !! Break your last record"; ; //On Android, this is considered as ContentText
        _notification.FireDate = System.DateTime.Now.AddSeconds(_fireAfterSec);
        _notification.RepeatInterval = _repeatInterval;
        _notification.UserInfo = _userInfo;
        _notification.SoundName = "pop.mp3"; //Keep the files in Assets/PluginResources/NativePlugins/Android or iOS or Common folder.

        _notification.iOSProperties = _iosProperties;
        _notification.AndroidProperties = _androidProperties;

        return _notification;
    }
}
