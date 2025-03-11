using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerSpeech : MonoBehaviour
{
    public TMP_Text playerSpeech;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ChangePlayerSpeech(string textToShow)
    {
        StartCoroutine(TextTimeout(textToShow));
    }

    public IEnumerator TextTimeout(string textToShow)
    {
        playerSpeech.text = textToShow.ToString();
        yield return new WaitForSeconds(5);
        playerSpeech.text = "";
    }
}
