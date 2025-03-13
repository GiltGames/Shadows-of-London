using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // making this script into a singleton pattern class
    // instance is the name we are giving the game object the script is attached to now
    public static MainManager Instance;
    public float musicVolume;
    public float sfxVolume;
    public float timeLeft;
    public float highScore;

    private void Awake()
    // awake function is called before anything else
    {
        // making sure that there is always one of this and destroying any copies
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        // making this object persistent
        DontDestroyOnLoad(gameObject);

    }

    # region Save time left to JSON
    [System.Serializable]
    class SaveData
    {
        public float _timeLeft;
    }

    public void SaveTime()
    {
        Debug.Log("running SaveTime");
        SaveData data = new SaveData();
        data._timeLeft = timeLeft;

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", jsonData);
        Debug.Log("New time saved: " + timeLeft);
    }

    public void LoadBestTime()
    {
        Debug.Log("running LoadBestTime");
        string path =  Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            Debug.Log("json file exists");
            string jsonData = File.ReadAllText(path);
            // deserialisation using SaveData template
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);

            highScore = data._timeLeft;
        }
        else {
            highScore = 0;
        }
    }
    # endregion
    

}
