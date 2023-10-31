using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinPoint : MonoBehaviour
{
    public Vector3 RespawnPosition;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestTimeText;

    private float startTime;
    private float bestTime;

    void Start()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        StartTimer();
        UpdateTimerUI();
        if (bestTime != float.MaxValue)
        {
            bestTimeText.text = FormatTime(bestTime);
        }
    }

    void Update()
    {
        UpdateTimerUI();
    }

    void OnDestroy()
    {
        PlayerPrefs.SetFloat("BestTime", bestTime);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("BestTime");  // Delete the best time when the application quits
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGameObject = collision.gameObject;
        if (collision.gameObject.name == "Player")
        {
            float currentTime = Time.time - startTime;
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                bestTimeText.text = FormatTime(bestTime);
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void StartTimer()
    {
        startTime = Time.time;
    }

    void UpdateTimerUI()
    {
        float currentTime = Time.time - startTime;
        timerText.text = FormatTime(currentTime);
        timerText.color = (currentTime > bestTime && bestTime != float.MaxValue) ? Color.red : Color.white;
    }

    string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)(time * 100) % 100;
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
