using UnityEngine;
using UnityEngine.Playables;

public class CriminalRunTriggetforCutScene : MonoBehaviour
{
    [SerializeField] GameObject[] gmoRunner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayableDirector>() != null)
        {

            for (int i = 0; i < gmoRunner.Length; i++)
            {
                gmoRunner[i].gameObject.SetActive(true);

            }

        }
    }


}
