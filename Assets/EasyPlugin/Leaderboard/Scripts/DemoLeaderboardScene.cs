using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoLeaderboardScene : MonoBehaviour
{
    public InputField ScoreField;

    public void ReportScore()
    {
        if (string.IsNullOrEmpty(ScoreField.text))
        {
            ScoreField.text = "0";
        }

        int score = int.Parse(ScoreField.text);

        // This line below is used to report the score to the leaderboards.
        LeaderboardManager.instance.ReportScore(score);
    }
}
