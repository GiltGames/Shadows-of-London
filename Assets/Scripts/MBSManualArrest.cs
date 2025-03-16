using System.Collections;
using TMPro;
using UnityEngine;

public class MBSManualArrest : MonoBehaviour
{
    // script is attached to the player (NOT the characters to be arrested)
    // originally the arrest was supposed to be triggered by clicking on a suspect.
    // the detection was erratic so now a suspect is selected by promity to the player 
    // and a click anywhere on the screen will acivate an arrest on that character


    [Header("Manual Arrest")]
    [SerializeField] float fltArrestPossibleAllowed = 15.0f;
   [SerializeField] float fltArrestTime = 4f;
    [SerializeField] Transform trnSuspect;
    [SerializeField] float fltDistancetoSuspect;
    [SerializeField] float fltAngletoSuspect;
    [SerializeField] float fltCheckInterval = 1f;
    [SerializeField] PlayerSpeech mbsPlaySpeech;
    [SerializeField] string strWords = "Arrest that Criminal!";
    [SerializeField] AudioClip audArrestCommand;
    [SerializeField] MBSAudioGlobal mbsAudio;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        // checks once per second for the character that could be arrested
        StartCoroutine(IECheckforSuspect());


        mbsPlaySpeech = FindFirstObjectByType<PlayerSpeech>();
        mbsAudio = FindFirstObjectByType<MBSAudioGlobal>();
        
    }

    // Update is called once per frame
    void Update()
    {


// arrest triggered on mouseclick

        if (Input.GetMouseButtonDown(0))
        {
            if (trnSuspect != null)
            {
                // if there is a current highlighted suspect, then the arrest functions trigger on the suspect character

                // truns of speech in the suspect, calls the arrest funtion on the target and deactivates the suspect as suspect 
                FnTurnOffSpeech();
                trnSuspect.gameObject.GetComponent<MBSArrestGuy>().FnArrested();
                trnSuspect = null;


                // player says "Arrest that Criminal"
                mbsPlaySpeech.ChangePlayerSpeech(strWords);
                mbsAudio.FnPlayGlobalAudio(audArrestCommand);

            }
        }

    }

    void FnSelectSuspect()
    {
        //checks each arrestable object
        


        MBSArrestGuy[] mbsPossibleSuspect   = Object.FindObjectsByType<MBSArrestGuy>(FindObjectsSortMode.None);

        // sets an initial detection angle to 60 degrees - a suspect has to be closer to directly forward than this to be selected

        fltAngletoSuspect = 60;
        if (trnSuspect != null)
        {
            FnTurnOffSpeech();
         
        }
        trnSuspect = null;


        //checks each arretable cahracter

        foreach (MBSArrestGuy possSuspect in mbsPossibleSuspect)
        {

            Vector3 vecPosSuspect = possSuspect.transform.position;
            Vector3 vecOffsettoSuspect = possSuspect.transform.position - transform.position;

            fltDistancetoSuspect = vecOffsettoSuspect.magnitude;
           
            //if arrestable object is in range check for angle
            
            if (fltDistancetoSuspect < fltArrestPossibleAllowed && !possSuspect.isArrested)
            {
                float fltAngletoSuspectTmp = Vector3.Angle(vecOffsettoSuspect, transform.forward);

                // select object with lowest angle
               if (fltAngletoSuspectTmp < fltAngletoSuspect)
                {
                    fltAngletoSuspect = fltAngletoSuspectTmp;
                    trnSuspect = possSuspect.transform;

                }



            }

            


        }

        if (trnSuspect != null)
        {
            trnSuspect.Find("Speech").gameObject.SetActive(true);
          

            // the whistle is above the head of the suspect and has a child spotlight
            // is inactive by default - switched on if the character is the suspect - ie the one closest to straight ahead.
            trnSuspect.Find("Whistle").gameObject.SetActive(true);
            
            // tuns off the suspect after a delay  - this only arises if no other suspect is detected.
            StartCoroutine(IEUnselect());


        }



    }


    IEnumerator IEUnselect()
    {

        //turns off suspect after delay

        yield return new WaitForSeconds(fltArrestTime);
        FnTurnOffSpeech();
        trnSuspect = null;


    }

    void FnTurnOffSpeech()
    {

        if (trnSuspect != null)
        {
            // deactivates the whislte

            trnSuspect.Find("Whistle").gameObject.SetActive(false);
            trnSuspect.Find("Speech").GetComponent<TextMeshPro>().text = "";
            trnSuspect.Find("Speech").gameObject.SetActive(false);
        }
    }


    IEnumerator IECheckforSuspect()
    {
        // co rountine runs continuously checking every second for a suspect

        while (true)
        {
            FnSelectSuspect();

            yield return new WaitForSeconds(fltCheckInterval);

        }

    }
}
