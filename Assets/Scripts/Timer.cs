using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image timerImage;
    public float timeLimit = 600.0f;
    public float timeLeft;
    public bool timeOut = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerImage.fillAmount = timeLeft / timeLimit;

        if (timeLeft <= 0)
        {
            timeOut = true;
        }
    }
}
