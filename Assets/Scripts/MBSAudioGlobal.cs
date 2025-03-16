using UnityEngine;

public class MBSAudioGlobal : MonoBehaviour
{
  
    // Plays the audio spoken by the player or her internal thoughts
    // attached to the Audio -Global game object
    
    [SerializeField] AudioSource audSource;
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audSource= GetComponent<AudioSource>();
    }

  

    public void FnPlayGlobalAudio(AudioClip clip)
    {
        audSource.clip = clip;
        audSource.Play();



    }

}
