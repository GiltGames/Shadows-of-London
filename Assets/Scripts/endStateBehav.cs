using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endStateBehav : MonoBehaviour
{
    public GameObject gameWinOverlay;
    public GameObject gameLoseOverlay;
    public GameObject staminaUI;
    public TMP_Text timeRemaining;
    public TMP_Text arrestsRemaining;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ActivateGameWin(float minsLeft, int arrestsLeft)
    {
        DisableHUD();
        gameWinOverlay.SetActive(true);
        timeRemaining.text = minsLeft + "";
        arrestsRemaining.text = arrestsLeft + "";
    }

    public void ActivateGameLose()
    {
        DisableHUD();
        gameLoseOverlay.SetActive(true);
    }

    void DisableHUD()
    {
        staminaUI.SetActive(false);

    }
}
