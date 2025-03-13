using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehav : MonoBehaviour
{

    public GameObject helpOverlay;
    public GameObject settingsOverlay;
    public GameObject mainButtons; 
    public GameObject currentUI;
    public GameObject HUD;      

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        HUD.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowHowToPlay()
    {
        mainButtons.SetActive(false);
        helpOverlay.SetActive(true);
        currentUI = helpOverlay;
    }

    public void OpenSettings()
    {
        mainButtons.SetActive(false);
        settingsOverlay.SetActive(true);
        currentUI = settingsOverlay; 
    }

    public void CloseButton()
    {
        currentUI.SetActive(false);
        mainButtons.SetActive(true);
    }
}
