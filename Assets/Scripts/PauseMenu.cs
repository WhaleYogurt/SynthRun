using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public TextMeshProUGUI bestTimeText;
    public GameObject resetTimeConfirmationUI;

    private bool isPaused = false;
    private string bestTimeKey;

    void Start()
    {
        bestTimeKey = SceneManager.GetActiveScene().name + "_BestTime";
        float bestTime = PlayerPrefs.GetFloat(bestTimeKey, float.MaxValue);

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        CancelReset();
        // Hide the cursor and lock it to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Show the cursor and unlock it
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowResetConfirmation()
    {
        resetTimeConfirmationUI.SetActive(true);
    }

    public void ConfirmReset()
    {
        PlayerPrefs.DeleteKey(bestTimeKey);
        bestTimeText.text = "00:00.00";
        resetTimeConfirmationUI.SetActive(false);
    }

    public void CancelReset()
    {
        resetTimeConfirmationUI.SetActive(false);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Stops the game in the Unity Editor
        #else
            Application.Quit();  // Quits the application when built
        #endif
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)(time * 100) % 100;
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
