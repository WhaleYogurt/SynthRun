using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class LevelSelection : MonoBehaviour
{
    public GameObject levelButtonPrefab;
    public Transform levelListParent;
    public GameObject levelSelectionPanel;

    public void ShowLevelSelection()
    {
        // Clear previous buttons
        foreach (Transform child in levelListParent)
        {
            Destroy(child.gameObject);
        }

        // Search the Assets directory for all scene files (*.unity)
        string[] sceneFiles = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);

        foreach (var sceneFile in sceneFiles)
        {
            string levelName = Path.GetFileNameWithoutExtension(sceneFile);

            // Only consider scenes that start with "Level"
            if (levelName.StartsWith("Level"))
            {
                GameObject buttonObj = Instantiate(levelButtonPrefab, levelListParent);
                TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();

                float bestTime = PlayerPrefs.GetFloat(levelName + "_BestTime", float.MaxValue);
                string bestTimeStr = (bestTime != float.MaxValue) ? FormatTime(bestTime) : "-";

                buttonText.text = $"{levelName}\nBest Time: {bestTimeStr}";

                // Assign the correct level to load when the button is clicked
                buttonObj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => LoadLevel(levelName));
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(levelName);
    }

    public void CloseLevelSelection()
    {
        // Close the level selection panel
        levelSelectionPanel.SetActive(false);
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)(time * 100) % 100;
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
