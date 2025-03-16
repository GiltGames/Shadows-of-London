using UnityEngine;

public class MBSAudioVolumeManager : MonoBehaviour
{
    
    // The volume settings are held on the singleton MainManager attached to the MainManager object
    // this reads all the settings from the main menu contained in main manager
    // all audio sources are set the the sfx volume, except the one tagged as music which is set to the music volume.
    //only runs at start of the main scene - no ability to update volume during the scene from the pause menu yet implemented
    //but it would be easy to add
    
    [SerializeField] MainManager mainManager;
       

    [SerializeField] 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      mainManager = FindFirstObjectByType<MainManager>();

        FnAudioUpdate();



    }

    public void FnAudioUpdate()
    {
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
