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
  
    [SerializeField] bool isArrested;
    [SerializeField] TMP_Text txtSpeech;
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

    }

    private void OnMouseOver()
    {



        if (Input.GetMouseButtonDown(1) && !isArrested)
        {

            FnArrested();
        }
    }

    public void FnArrested()
    {
        fltDistance = 1000;

        foreach (MBSPoliceGuy policeman in mbsPoliceGuy)
        {
            bool isArrestingTmp = policeman.GetComponent<MBSPoliceGuy>().isArresting;

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

        txtSpeech.text = "Arrested by "+ trnClosestPolice.name;

        isArrested = true;


    }

    public void FnInCustody()
    {
        
        
      

        if (mbsNav != null)
        {
            mbsNav.agent.SetDestination(trnCustodyLocation.position);
            mbsNav.anim.SetBool("Still", false);

            mbsNav.isWaiting = false;
            mbsNav.isWanderingMode = false;
            mbsNav.fltDelayCount = 0;
            
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

}
