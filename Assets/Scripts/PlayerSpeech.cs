using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerSpeech : MonoBehaviour
{
    public TMP_Text playerSpeech;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpeech = GetComponent<TMP_Text>();
    }

    public void DisplayText(string textToShow)
    {
        TextTimeout(textToShow);
    }

    IEnumerator TextTimeout(string textToShow)
    {
        playerSpeech.text = textToShow;
        playerSpeech.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        playerSpeech.gameObject.SetActive(false);
    }
}
