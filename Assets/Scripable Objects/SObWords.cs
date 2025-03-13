using UnityEngine;

[CreateAssetMenu(fileName = "SObWords", menuName = "Scriptable Objects/Words")]
public class SObWords : ScriptableObject
{
    public string strArrestWords;
    public string strIDwords;
    public string strGotAwayWords;
    public string[] strRandomWords;
    public AudioClip audArrestWords;
    public AudioClip audGotAwayWords;
    public AudioClip[] audRandomWords;
    public AudioClip audIDwords;
    public float fltRandomSpeechBaseInterval;
    public float fltRandomSpeechRandomMultiple;
}
