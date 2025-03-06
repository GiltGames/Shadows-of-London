using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MBSArrestGuy : MonoBehaviour
{
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

    [SerializeField] Animator aniPlayer;


    [Header("Evade")]
    [SerializeField] float fltReacttoPoliceDistance;
    [SerializeField] Transform trnTargetTemp;
    [SerializeField] float fltWalkAwayDistance;
    [SerializeField] float fltEvadeTimer;
    [SerializeField] float fltEvadeInterval;
    [SerializeField] float fltClosestDistanceforEvade;

    




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        mbsPoliceGuy = Object.FindObjectsByType<MBSPoliceGuy>(FindObjectsSortMode.None);    
        mbsNav = GetComponent<MBSBasicNavigationGUy>();

        if (mbsNav == null)
        {
            mbsFollower = GetComponent<MBSFollowerGuy>();


        }
        

    }

    // Update is called once per frame
    void Update()
    {
        // EVADES if not arrested and is criminal or Neerdowell

        if (mbsNav != null)
        {
            if (!isArrested)
            {

                if (mbsNav.isCriminal || mbsNav.isNeerdoWell)
                {
                    fltEvadeTimer += Time.deltaTime;
                    if (fltEvadeTimer > fltEvadeInterval)
                    {
                        fltEvadeTimer = 0;
                        FnEvade();
                    }
                }
            }

           
        }

        if (!isArrested)
        {
          //  FnRayArrest();
        // redundant raycast check to see if the mouse is on    
            

        }





    }

    private void OnMouseOver()
    {
        gmoHighlight.SetActive(true);


        if (Input.GetMouseButtonDown(1) && !isArrested)
        {

            FnArrested();
        }
    }

    private void OnMouseExit()
    {
        gmoHighlight.SetActive(false);
    }


    public void FnArrested()
    {
        fltDistance = 1000;

        aniPlayer.SetTrigger("isArresting");

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

        mbsClosestPolice = trnClosestPolice.GetComponent<MBSPoliceGuy>();
        
        
        mbsClosePoliceNav = trnClosestPolice.GetComponent <MBSBasicNavigationGUy>();
        mbsClosePoliceNavFollow = trnClosestPolice.GetComponent<MBSFollowerGuy>();
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
      

        isArrested = true;


    }

    public void FnInCustody()
    {

        gmoSpeech.SetActive(true);
        txtSpeech.text = "You have got me, Mr  " + trnClosestPolice.name;
        StartCoroutine(IESpeechOff());


        if (mbsNav != null)
        {
            mbsNav.agent.SetDestination(trnCustodyLocation.position);
            mbsNav.anim.SetBool("Still", false);
            Debug.Log("Arrested with destination set at " + trnCustodyLocation.position);


            mbsNav.isWaiting = false;
            mbsNav.isWanderingMode = false;
           
            
        }

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


        yield return new WaitForSeconds(fltSpeechTime);

        gmoSpeech.SetActive(false);

    }

}
