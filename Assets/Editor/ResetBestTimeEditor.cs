#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class ResetBestTimeEditor
{
    [MenuItem("Tools/Reset All Best Times")]
    public static void ResetAllBestTimes()
    {
        // Find all scene files in the project
        string[] sceneFiles = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);

        foreach (string sceneFile in sceneFiles)
        {
            string sceneName = Path.GetFileNameWithoutExtension(sceneFile);
            string bestTimeKey = sceneName + "_BestTime";

            if (PlayerPrefs.HasKey(bestTimeKey))
            {
                PlayerPrefs.DeleteKey(bestTimeKey);
            }
        }
    }
}
#endif
