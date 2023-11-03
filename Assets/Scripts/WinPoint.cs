using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // Import this namespace

public class WinPoint : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestTimeText;
    private float startTime;
    private float bestTime;
    private bool timerActive = true;
    private string bestTimeKey;  // Key used to save the best time
    public LevelTimeTracker levelTimeTracker;

    void Start()
    {
        bestTimeKey = SceneManager.GetActiveScene().name + "_BestTime";  // Unique key for each scene
        bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);
        StartTimer();

        UpdateTimerUI();

        // Display the best time if it exists, otherwise display a placeholder
        if (bestTime != float.MaxValue)
        {
            bestTimeText.text = FormatTime(bestTime);
        }
        else
        {
            bestTimeText.text = "00:00.00";
        }
    }


    void Update()
    {
        bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);
        if (bestTime != float.MaxValue)
        {
            bestTimeText.text = FormatTime(bestTime);
        }
        else
        {
            bestTimeText.text = "00:00.00";
        }
        if (timerActive)
        {
            UpdateTimerUI();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGameObject = collision.gameObject;
        if (collision.gameObject.name == "Player")
        {
            timerActive = false;
            float currentTime = Time.time - startTime;
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat(bestTimeKey, bestTime);

                // Update the displayed best time immediately
                bestTimeText.text = FormatTime(bestTime);
            }
            levelTimeTracker.OnLevelCompleted(currentTime, bestTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            StartTimer();
        }
    }


    private void StartTimer()
    {
        startTime = Time.time;
        timerActive = true;
    }

    private void UpdateTimerUI()
    {
        float currentTime = Time.time - startTime;
        timerText.text = FormatTime(currentTime);
        timerText.color = (currentTime > bestTime) ? Color.red : Color.white;
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)(time * 100) % 100;
        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
}
