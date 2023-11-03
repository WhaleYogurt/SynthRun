using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;

public class EditorTools : MonoBehaviour
{
    private const string timeKeyBase = "LevelCompletionTime";
    private const int maxAttempts = 10;

    [MenuItem("Tools/Reset Leaderboard for Current Level")]
    public static void ResetCurrentLevelLeaderboard()
    {
        // Confirm the action with the user
        if (EditorUtility.DisplayDialog("Reset Leaderboard",
                                        "Are you sure you want to reset the leaderboard for the current level?",
                                        "Yes", "No"))
        {
            // Get the current scene's name
            string currentSceneName = SceneManager.GetActiveScene().name;

            // Construct the key for PlayerPrefs
            string timeKey = timeKeyBase + "_" + currentSceneName;

            // Remove the saved times for the current level
            for (int i = 0; i < maxAttempts; i++)
            {
                PlayerPrefs.DeleteKey(timeKey + i);
            }

            // Notify the user
            Debug.Log("Leaderboard for " + currentSceneName + " has been reset.");
        }
    }

    [MenuItem("Tools/Reset Everything")]
    public static void ResetEverything()
    {
        // Confirm the action with the user
        if (EditorUtility.DisplayDialog("Reset All Leaderboards, Best Times, ect",
                                        "Are you sure you want to reset everything?",
                                        "Yes", "No"))
        {
            PlayerPrefs.DeleteAll();
            // Notify the user
            Debug.Log("Everything has been reset.");
        }
    }

    [MenuItem("Tools/Reset All Leaderboards")]
    public static void ResetAllLeaderboards()
    {
        // Confirm the action with the user
        if (EditorUtility.DisplayDialog("Reset All Leaderboards",
                                        "Are you sure you want to reset all leaderboards?",
                                        "Yes", "No"))
        {
            // Get all scene names
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            for (int i = 0; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                // Construct the key for PlayerPrefs
                string timeKey = timeKeyBase + "_" + sceneName;

                // Remove the saved times for each level
                for (int j = 0; j < maxAttempts; j++)
                {
                    PlayerPrefs.DeleteKey(timeKey + j);
                }
            }
            // Notify the user
            Debug.Log("All leaderboards have been reset.");
        }
    }
    [MenuItem("Tools/Reset All Best Times")]
    public static void ResetAllBestTimes()
    {
        if (EditorUtility.DisplayDialog("Reset All Best Time",
                                        "Are you sure you want to reset the best times of all levels currently in the game?",
                                        "Yes", "No"))
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
            // Notify the user
            Debug.Log("All best times have been reset.");
        }
    }
}
