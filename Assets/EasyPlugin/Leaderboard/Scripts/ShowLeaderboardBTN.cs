using UnityEngine;
using UnityEngine.UI;

public class ShowLeaderboardBTN : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        EasyBrainPluginManager.instance.PlayClickSound();
        Social.ShowLeaderboardUI();
    }
}
