using UnityEngine;

public class MBSAudioGlobal : MonoBehaviour
{
    [SerializeField] AudioSource audSource;
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audSource= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FnPlayGlobalAudio(AudioClip clip)
    {
        audSource.clip = clip;
        audSource.Play();



    }

}
