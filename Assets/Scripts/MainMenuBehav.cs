using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehav : MonoBehaviour
{

    public GameObject helpOverlay;
    public GameObject settingsOverlay; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

    }

    public void OpenSettings()
    {

    }
}
