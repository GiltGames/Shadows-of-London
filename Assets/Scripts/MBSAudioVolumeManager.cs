using UnityEngine;

public class MBSAudioVolumeManager : MonoBehaviour
{
    [SerializeField] MainManager mainManager;
    public float volMusic;
    public float volSFX;
    [SerializeField] 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      mainManager = FindFirstObjectByType<MainManager>();
        
        volMusic = mainManager.musicVolume;
        volSFX = mainManager.sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
