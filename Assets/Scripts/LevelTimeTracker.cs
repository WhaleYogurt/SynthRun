using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // Import this namespace

public class LevelTimeTracker : MonoBehaviour
{
    public TMP_Text timeDisplay;  // TMP Text component to display the times
    private float startTime;  // Time when the level started
    private const string timeKeyBase = "LevelCompletionTime";  // PlayerPrefs key
    private string timeKey;
    private const int maxAttempts = 10;  // Number of attempts to track

    void Start()
    {
        // Initialize the start time
        startTime = Time.time;
        string currentSceneName = SceneManager.GetActiveScene().name;
        timeKey = timeKeyBase + "_" + currentSceneName;
        // Display previous times
        UpdateTimeDisplay(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_BestTime", float.MaxValue));
    }

    // Call this function when the level is completed
    public void OnLevelCompleted(float completionTime, float bestTime)
    {
        SaveCompletionTime(completionTime);  // Save the time
        UpdateTimeDisplay(bestTime);  // Update the display
        // Optionally, you can restart the level or load the next level here
    }

    // Saves the completion time in PlayerPrefs
    private void SaveCompletionTime(float completionTime)
    {
        // Load previous times
        float[] previousTimes = new float[maxAttempts];
        for (int i = 0; i < maxAttempts; i++)
        {
            previousTimes[i] = PlayerPrefs.GetFloat(timeKey + i, -1);
        }

        // Shift the times and add the new time
        for (int i = maxAttempts - 1; i > 0; i--)
        {
            previousTimes[i] = previousTimes[i - 1];
        }
        previousTimes[0] = completionTime;

        // Save the updated times
        for (int i = 0; i < maxAttempts; i++)
        {
            if (previousTimes[i] != -1)
                PlayerPrefs.SetFloat(timeKey + i, previousTimes[i]);
        }
    }

    // Updates the time display
    private void UpdateTimeDisplay(float bestTime)
    {
        string displayText = "";
        for (int i = 0; i < maxAttempts; i++)
        {
            float time = PlayerPrefs.GetFloat(timeKey + i, -1);
            if (time != -1)
            {
                displayText += FormatTime(time) + " ";
                float dif = Mathf.Round((time - bestTime) * 100) / 100;
                if (dif == 0)
                {
                    displayText += "<color=green> Best Time</color>\n";
                }
                else if (dif > 0)
                {
                    displayText += "<color=red> +" + dif + "</color>\n";
                }
            }

        }
        timeDisplay.text = displayText;
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)(time * 100) % 100;
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}
