using UnityEngine;

public class MBSAudioVolumeManager : MonoBehaviour
{
    [SerializeField] MainManager mainManager;
    
    

    [SerializeField] 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      mainManager = FindFirstObjectByType<MainManager>();

        AudioSource[] audSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource audSource in audSources)
        {
            float vol = mainManager.sfxVolume;
            if (audSource.transform.tag == "Music")
            {
                vol = mainManager.musicVolume;
            }

            FnSetVolume(audSource, vol);
        }    



    }



    public void FnSetVolume(AudioSource aSource, float vol)
    {
        aSource.volume = vol;


    }


}
