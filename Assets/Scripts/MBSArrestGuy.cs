using System.Collections;
using TMPro;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MBSArrestGuy : MonoBehaviour
{

    //script is attached to each arrestable character, ie crowd and criminals and neerdowells


    [SerializeField] MBSPoliceGuy[] mbsPoliceGuy;
    [SerializeField] float fltDistance;
    [SerializeField] Transform trnClosestPolice;
    [SerializeField] MBSPoliceGuy mbsClosestPolice;
    [SerializeField] MBSBasicNavigationGUy mbsClosePoliceNav;
    [SerializeField] MBSFollowerGuy mbsClosePoliceNavFollow;
   [SerializeField] MBSBasicNavigationGUy mbsNav;
    [SerializeField] MBSFollowerGuy mbsFollower;
    [SerializeField] Transform trnCustodyLocation;
  [Header ("Arrest")]
    public bool isArrested;
    [SerializeField] TMP_Text txtSpeech;
    [SerializeField] float fltSpeechTime=1.5f;
    [SerializeField] float fltSpeechCounter;
    [SerializeField] GameObject gmoSpeech;
 public Camera playerCamera;
    public float lineRange = 50f;
    public static Vector3 hitPosition;
    public GameObject gmoHighlight;
    [SerializeField] MBSArrestUpdateUI mbsArrestUI;
    [SerializeField] MBSCriminalUnID mbsCrim;





    [SerializeField] Animator aniPlayer;


    [Header("Evade")]
    [SerializeField] float fltReacttoPoliceDistance;
    [SerializeField] Transform trnTargetTemp;
    [SerializeField] float fltWalkAwayDistance;
    [SerializeField] float fltEvadeTimer;
    [SerializeField] float fltEvadeInterval;
    [SerializeField] float fltClosestDistanceforEvade;

    [Header("Audio")]
    [SerializeField] SObWords sobWords;
  
    [SerializeField] AudioSource audSource;
    [SerializeField] AudioClip audGroan;
    



    void Start()
    {
        // find all the police in the scene for later use
        mbsPoliceGuy = Object.FindObjectsByType<MBSPoliceGuy>(FindObjectsSortMode.None);    
        
        // checks for attached scripts, basic navigation, criminal and arrestUI - which is only present on criminals

        mbsNav = GetComponent<MBSBasicNavigationGUy>();

       if (GetComponent<MBSCriminalUnID>() != null)
        {
            mbsArrestUI = GetComponent<MBSArrestUpdateUI>();
            mbsCrim = GetComponent<MBSCriminalUnID>();
        }


        if (mbsNav == null)
        {
            mbsFollower = GetComponent<MBSFollowerGuy>();


        }
        audSource = GetComponent<AudioSource>();

        // speaks random phrases
        StartCoroutine(IERandomSpeech());

    }

    // Update is called once per frame
    void Update()
    {
        // EVADES if not arrested and is criminal or Neerdowell - this was removed - check on github history for this

        





    }

   

  

    public void FnArrested()
    {
       
        // function called if an arrest is called for on this character
        
        fltDistance = 1000;

        aniPlayer.SetTrigger("isArresting");

        //finds closest policeman

        foreach (MBSPoliceGuy policeman in mbsPoliceGuy)
        {
            bool isArrestingTmp = policeman.GetComponent<MBSPoliceGuy>().isArresting;
            if ( policeman.GetComponent<MBSPoliceGuy>().isHasSomeoneInCustody)
            {
                isArrestingTmp = true;
            }

            Vector3 vectortmp = (policeman.transform.position- transform.position);
            if ( vectortmp.magnitude < fltDistance && !isArrestingTmp)
            {
                trnClosestPolice = policeman.transform;

                fltDistance = vectortmp.magnitude;

            }
            
            


        }

        // if there is a closest policeman who has not already been used for an arrest then triggers arrest fucntions int he MBSPOlice script

        if (trnClosestPolice != null)
        {


            mbsClosestPolice = trnClosestPolice.GetComponent<MBSPoliceGuy>();


            mbsClosePoliceNav = trnClosestPolice.GetComponent<MBSBasicNavigationGUy>();
            mbsClosePoliceNavFollow = trnClosestPolice.GetComponent<MBSFollowerGuy>();

            //check to see if the policemna is a base or a follower and call equivalent provisions

            if (mbsClosePoliceNav != null)
            {
                mbsClosePoliceNav.isWanderingMode = false;
                mbsClosePoliceNav.anim.SetBool("Still", false);
                mbsClosePoliceNav.anim.SetBool("Run", true);

            }

            if (mbsClosePoliceNavFollow != null)
            {
                mbsClosePoliceNavFollow.isWanderingMode = false;
                mbsClosePoliceNavFollow.anim.SetBool("Still", false);
                mbsClosePoliceNavFollow.anim.SetBool("Run", true);

            }

            mbsClosestPolice.isArresting = true;
            mbsClosestPolice.trnPersonArrested = transform;
            mbsClosestPolice.audSource.clip = mbsClosestPolice.audOntheCase;
            mbsClosestPolice.audSource.Play();


            isArrested = true;
            
           //calls on data in the scriptable object for the character type for their reaction tot he arrest
            FnSpeak(sobWords.strIDwords, sobWords.audIDwords);

           
        }

        else
        {
            FnSpeak("There are no more police - damn it!", audGroan);

            FindFirstObjectByType<endStateBehav>().ActivateGameNoPolice();
            
            //no more police

        }

    }

    public void FnInCustody()
    {
        // triggered when the arresting policeman gets close enough to arrest someone


        // sends string and audio clip to the speech function
        //calls on data in the scriptable object for the character type for their reaction to being taken into custody
        FnSpeak(sobWords.strArrestWords, sobWords.audArrestWords);
        
       

      

        if (mbsNav != null)
        {
            if (mbsNav.isCriminal)
            {

                if (mbsCrim.isDetectable)
                {

                    // if the arrested character is a criminal who has theirhint switched on then update the UI

                    mbsArrestUI.FnArrestUpdateUI(mbsCrim.intCriminalIndex);

                }
            }

            // arrested character walks back to custody poistion useing the navmesh

            mbsNav.agent.SetDestination(trnCustodyLocation.position);
            mbsNav.anim.SetBool("Still", false);
            Debug.Log("Arrested with destination set at " + trnCustodyLocation.position);


            mbsNav.isWaiting = false;
            mbsNav.isWanderingMode = false;
           
            
        }

        // equivalent if the arrested character has a following script, so follows another crowd memmerb - these can't be criminals

        if (mbsFollower != null)
        {
            mbsFollower.agent.SetDestination(trnCustodyLocation.position);

            mbsFollower.anim.SetBool("Still", false);
            mbsFollower.isWanderingMode = false;
            mbsFollower.isWaiting = false;
           

        }

       
    }


    void FnEvade()
    {
        //redundant as this did not work well and appeared to confuse the use of the navmesh - the call was originall in the update function.


        fltClosestDistanceforEvade = 1000;
        foreach (MBSPoliceGuy policeman in mbsPoliceGuy)
        {


            Vector3 vectortmp = (policeman.transform.position - transform.position);
            if (vectortmp.magnitude < fltReacttoPoliceDistance && vectortmp.magnitude < fltClosestDistanceforEvade)
            {
                trnClosestPolice = policeman.transform;
                fltClosestDistanceforEvade = vectortmp.magnitude;

            }

        }

        if (fltClosestDistanceforEvade < 900)
        {
            trnTargetTemp.position = transform.position+ (trnClosestPolice.position - transform.position).normalized * - fltWalkAwayDistance;

            mbsNav.trnCurrentTarget = trnTargetTemp;
            mbsNav.agent.SetDestination(trnTargetTemp.position);
            mbsNav.vecNavTarget = trnTargetTemp.position;
            GetComponent<MBSCriminalUnID>().isTryingtoMakeProgress = false;
            
            mbsNav.anim.SetBool("Still" ,false);
            mbsNav.isWaiting = false;   

        }

        


    }


    void FnRayArrest()
    {

        //redundant, from when the arrest was triggered by a right click when over a character uses raycast to detect who was hit.
        //this ewas unreliable so was dropped in favour of the MBSArrestManual script -
        // this code is never called but has been left in just in case this function is reactivated.

        Vector3 mousePos = Input.mousePosition;


        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit, lineRange))
        {
            if (hit.collider.transform == transform)
            {
                gmoHighlight.SetActive(true);

                if (Input.GetMouseButtonDown(1))
                {

                    FnArrested();
                }


            }

            else
            {
                gmoHighlight.SetActive(false);
            }

            hitPosition = hit.point;
            //Debug.Log("Hit position " + hit.point);
            Debug.Log("Hit object: " + hit.collider.name);
        }


    }

    IEnumerator IESpeechOff()
    {
        // switches the speech object on the character off after a time

        yield return new WaitForSeconds(fltSpeechTime);

        gmoSpeech.SetActive(false);

    }


    IEnumerator IERandomSpeech()
    {

        // runs throughout the game 
        //random speech from the scriptable object is displayed as text above the character and the audio dialogue is played

        while (true)
        {
            yield return new WaitForSeconds(sobWords.fltRandomSpeechBaseInterval * (1 + Random.Range(0, sobWords.fltRandomSpeechRandomMultiple)));

            int wordchoice = Random.Range(0, sobWords.strRandomWords.Length);

         
           FnSpeak(sobWords.strRandomWords[wordchoice], sobWords.audRandomWords[wordchoice]);
      



        }

       
    }

    public void FnSpeak(string words, AudioClip audWords)
    {
        //The actual code to display the speech and play the dialogue audio.

        gmoSpeech.SetActive(true);
        txtSpeech.text = words;
        //play audio to add
        audSource.clip = audWords;
        audSource.Play();


        StartCoroutine(IESpeechOff());


    }


}
