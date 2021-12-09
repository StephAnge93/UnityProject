using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class StartFromSplash : MonoBehaviour
{
    [MenuItem("Helpers/Reset Data")]
    public static void Reset()
    {
        PlayerPrefs.DeleteAll();

        if (EditorApplication.isPlaying)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    [MenuItem("Helpers/AddCoin")]
    public static void AddCoin()
    {
        CreditManager.AddBalance(0, 100);
    }
}
