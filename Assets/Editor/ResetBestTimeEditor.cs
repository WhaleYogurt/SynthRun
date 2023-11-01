using UnityEngine;
using UnityEditor;

public class ResetBestTimeEditor : MonoBehaviour
{
    [MenuItem("Tools/Reset Best Time")]
    public static void ResetBestTime()
    {
        PlayerPrefs.DeleteKey("BestTime");
    }
}
 