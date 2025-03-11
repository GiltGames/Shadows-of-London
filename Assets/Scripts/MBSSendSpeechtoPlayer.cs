using System.Collections;
using UnityEngine;

public class MBSSendSpeechtoPlayer : MonoBehaviour
{
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

        mbsPlaySpeech.ChangePlayerSpeech(strWords);


        if (audClip != null)
        {
            mbsAudio.FnPlayGlobalAudio(audClip);

        }

        StartCoroutine(IEDisable());

    }


    IEnumerator IEDisable()
    {
        yield return new WaitForSeconds(fltTriggerRemoveDelay);
        gameObject.SetActive(false);


    }


}
