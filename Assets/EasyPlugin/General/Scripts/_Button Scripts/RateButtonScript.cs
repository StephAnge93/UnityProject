using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RateButtonScript : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        EasyBrainPluginManager.instance.PlayClickSound();
        EasyBrainPluginManager.instance.OnRateClick();
    }
}
