using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayableDirector>() != null)
        {

            SceneManager.LoadScene(2);




        }



    }

}