using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameInfo")]
public class GameInfo : ScriptableObject
{
    public string Leaderboard_Id_android;
    public string Leaderboard_Id_iOS;

    [Space(20)]
    public string ShareLink_iOS;
    public string ShareLink_android;

    [Space(20)]
    public string RateUs_iOS;
    public string RateUs_Android;

    [Space(20)]
    public int InitialCoinBalance;
}
