using System.Collections;
using TMPro;
using UnityEngine;

public class MBSManualArrest : MonoBehaviour
{


    [Header("Manual Arrest")]
    [SerializeField] float fltArrestPossibleAllowed = 15.0f;
   [SerializeField] float fltArrestTime = 4f;
    [SerializeField] Transform trnSuspect;
    [SerializeField] float fltDistancetoSuspect;
    [SerializeField] float fltAngletoSuspect;
    [SerializeField] float fltCheckInterval = 1f;
    [SerializeField] PlayerSpeech mbsPlaySpeech;
    [SerializeField] string strWords = "Arrest that Criminal!";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(IECheckforSuspect());
        mbsPlaySpeech = FindFirstObjectByType<PlayerSpeech>();

    }

    // Update is called once per frame
    void Update()
    {

        // Arrest Check on pressing F -


       /*if (Input.GetKeyDown(KeyCode.P))
        {
            

           
            { 
            FnSelectSuspect();

                }

        }
       */



        if (Input.GetMouseButtonDown(0))
        {
            if (trnSuspect != null)
            {
                FnTurnOffSpeech();
                trnSuspect.gameObject.GetComponent<MBSArrestGuy>().FnArrested();
                trnSuspect = null;

                mbsPlaySpeech.ChangePlayerSpeech(strWords);

            }
        }
/*
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (trnSuspect != null)
            {
                FnTurnOffSpeech();
                trnSuspect = null;
            }
        }
*/
    }

    void FnSelectSuspect()
    {
        //checks each arrestable object
        
        MBSArrestGuy[] mbsPossibleSuspect   = Object.FindObjectsByType<MBSArrestGuy>(FindObjectsSortMode.None);

        fltAngletoSuspect = 60;
        if (trnSuspect != null)
        {
            FnTurnOffSpeech();
         
        }
        trnSuspect = null;

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
          // trnSuspect.Find("Speech").GetComponent<TextMeshPro>().text = "F";
            trnSuspect.Find("Whistle").gameObject.SetActive(true);
            StartCoroutine(IEUnselect());


        }



    }


    IEnumerator IEUnselect()
    {

        yield return new WaitForSeconds(fltArrestTime);
        FnTurnOffSpeech();
        trnSuspect = null;


    }

    void FnTurnOffSpeech()
    {

        if (trnSuspect != null)
        {
            trnSuspect.Find("Whistle").gameObject.SetActive(false);
            trnSuspect.Find("Speech").GetComponent<TextMeshPro>().text = "";
            trnSuspect.Find("Speech").gameObject.SetActive(false);
        }
    }


    IEnumerator IECheckforSuspect()
    {
        while (true)
        {
            FnSelectSuspect();

            yield return new WaitForSeconds(fltCheckInterval);

        }

    }
}
