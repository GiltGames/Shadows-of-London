using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    // triggers main scene when the camera enters the trigger at the doorway

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayableDirector>() != null)
        {

            SceneManager.LoadScene(2);




        }



    }

}