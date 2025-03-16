using System.Collections;
using UnityEngine;

public class MBSSendSpeechtoPlayer : MonoBehaviour
{

    // this attaches to teh triggers set the in the scene
    // sends the text to the ChangePlayerSpeech script an the audio to the Audio Global screipt
    // text and audio defined in the inspector

    [SerializeField] PlayerSpeech mbsPlaySpeech;
    [TextArea (1,10)]
    [SerializeField] string strWords;
    [SerializeField] MBSAudioGlobal mbsAudio;
    [SerializeField] AudioClip audClip;
    [SerializeField] float fltTriggerRemoveDelay = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsPlaySpeech = FindFirstObjectByType<PlayerSpeech>();
        mbsAudio = FindFirstObjectByType<MBSAudioGlobal>();
    }

    private void OnTriggerEnter(Collider other)
    {

        // detects if it is the player that his the tirrger

        if (other.GetComponent<PlayerMovement>() != null)
        {

            // sends text and audio to relevant functions
            


            mbsPlaySpeech.ChangePlayerSpeech(strWords);


            if (audClip != null)
            {
                mbsAudio.FnPlayGlobalAudio(audClip);

            }

            StartCoroutine(IEDisable());
        }
    }


    IEnumerator IEDisable()
    {

        // truns off the trigger so it only rns once
        yield return new WaitForSeconds(fltTriggerRemoveDelay);
        gameObject.SetActive(false);


    }


}
