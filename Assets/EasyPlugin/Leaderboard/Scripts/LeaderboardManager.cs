using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

//#if UNITY_ANDROID
//using GooglePlayGames;
//#endif

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        Initialise();
    }

    public void Initialise()
    {
        //#if UNITY_ANDROID
        //        PlayGamesPlatform.Activate();
        //#endif

        Social.localUser.Authenticate((success)=>{ });
    }

    public int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt("best_score");
        }
        set
        {
            PlayerPrefs.SetInt("best_score", value);
        }
    }

    public bool IsNewHighScore(int score)
    {
        return score > BestScore;
    }

    public void ReportScore(int score){
        string leaderboardid = string.Empty;

        #if UNITY_ANDROID
                leaderboardid = EasyBrainPluginManager.instance.gameInfo.Leaderboard_Id_android;
        #elif UNITY_IOS
                        leaderboardid = EasyBrainPluginManager.instance.gameInfo.Leaderboard_Id_iOS;
        #endif

        Debug.Log("Sending score : " + score + " to leaderboard : " + leaderboardid);

        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
            if (success)
            {
                Social.ReportScore(score, leaderboardid, (bool success2) => {
                    // handle success or failure
                });
            }
        });
    }
}
