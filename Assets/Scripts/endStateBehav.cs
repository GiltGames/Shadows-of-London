using System.Timers;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endStateBehav : MonoBehaviour
{
    public GameObject gameWinOverlay;
    public GameObject gameLoseOverlay;
    public GameObject gamePartialWinOverlay;
    public GameObject gamePauseOverlay;
    public GameObject staminaUI;
    public TMP_Text timeRemaining;
    public TMP_Text arrestsRemaining;
    public GameObject highscoreNotification;

    AddToInventory addToInvScript;
    Timer timerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        addToInvScript = GetComponentInParent<AddToInventory>();
        timerScript = FindFirstObjectByType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerScript.timeOut == true)
        {
            // partial win if some enemies caught when timer ran out
            if (addToInvScript.enemiesCaught > 0 )
            {
                ActivateGamePartialWin();
            }
            else
            {
                ActivateGameLose();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }

        // this can be removed after debugging as seems more efficient to call it when enemiesCaught is updated in AddToInventory.cs rather than checking here every frame
        if (addToInvScript.enemiesCaught == 6)
        {
            ActivateGameWin();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    //public void ActivateGameWin(float minsLeft, int arrestsLeft)
    public void ActivateGameWin()
    {
        Debug.Log("activate game win");
        DisableHUD();
        gameWinOverlay.SetActive(true);
        timeRemaining.text = timerScript.timeLeft.ToString("F1");
        // 1 is a placeholder. Replace with arrests left
        arrestsRemaining.text = 1.ToString();
        ActivateSaveTime();
    }

    public void ActivateGameLose()
    {
        Debug.Log("activate game lose");
        DisableHUD();
        gameLoseOverlay.SetActive(true);
    }

    public void ActivateGamePartialWin()
    {
        Debug.Log("activate game partial win");
        DisableHUD();
        gamePartialWinOverlay.SetActive(true);
    }

    void DisableHUD()
    {
        staminaUI.SetActive(false);
        Time.timeScale = 0;
    }

    public void ShowPauseMenu()
    {
        gamePauseOverlay.SetActive(true);
        DisableHUD();
    }

    public void ResumeGame()
    {
        gamePauseOverlay.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ActivateSaveTime()
    {
        MainManager.Instance.timeLeft = timerScript.timeLeft;
        MainManager.Instance.LoadBestTime();
        // save the time left only if it is higher than the previous high score
        if (MainManager.Instance.highScore < MainManager.Instance.timeLeft) 
        {
            MainManager.Instance.SaveTime();
            highscoreNotification.SetActive(true);
        }
    }

}
